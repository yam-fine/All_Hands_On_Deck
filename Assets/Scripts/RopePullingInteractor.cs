using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//using System.Linq;

public class RopePullingInteractor : MonoBehaviour
{
    //[SerializeField] private GameObject _grabbingPoint;

    //private Vector3 startPos;
    //private float baseLength;

    //private void Start()
    //{
    //    startPos = _grabbingPoint.transform.position;
    //    baseLength = transform.localScale.y;
    //}

    //void Update()
    //{
    //    if( _grabbingPoint is null)
    //    {
    //        return;
    //    }

    //    float scaleChange = (_grabbingPoint.transform.position - startPos).y;
    //    transform.localScale = new Vector3 (transform.localScale.x, Mathf.Max(baseLength, baseLength - scaleChange), transform.localScale.z);
    //}
    [SerializeField] private GameObject pole; // Reference to the pole
    [SerializeField] private XRGrabInteractable[] grabbingPoints; // Array of grabbing points

    private Vector3[] startPositions; // Store the start positions of each grabbing point
    private float baseLength;
    private bool isReturning = false;
    [SerializeField] private XRGrabInteractable activeGrabbingPoint = null;
    private float currentLength;
    private Coroutine release;

    private Vector3 previousPosition;
    private Vector3 releaseVelocity = Vector3.zero;
    [SerializeField] private float velocityDampMultiplier = 0.5f;
    [SerializeField] private float velocityDecayRate = 30f;
    [SerializeField] private float returningSpeed = 4.0f;
    [SerializeField] private Animator sailAnimator;
    [SerializeField] private float sailOpenPoint;


    private void Start()
    {
        baseLength = pole.transform.localScale.y;
        currentLength = baseLength;

        // Initialize the start positions array
        startPositions = new Vector3[grabbingPoints.Length];
        for (int i = 0; i < grabbingPoints.Length; i++)
        {
            startPositions[i] = grabbingPoints[i].transform.position;

            // Add event listeners for the SelectEnter and SelectExit events
            grabbingPoints[i].selectEntered.AddListener(OnGrabbingPointGrabbed);
            grabbingPoints[i].selectExited.AddListener(OnGrabbingPointReleased);
        }
    }

    void Update()
    {
        if (activeGrabbingPoint != null)
        {
            // Scale the pole based on the active grabbing point's position
            float scaleChange = (activeGrabbingPoint.transform.position - startPositions[System.Array.IndexOf(grabbingPoints, activeGrabbingPoint)]).y;
            currentLength = Mathf.Max(baseLength, baseLength - scaleChange);
            pole.transform.localScale = new Vector3(pole.transform.localScale.x, currentLength, pole.transform.localScale.z);

            // Adjust the position of all grabbing points based on the scale change
            for (int i = 0; i < grabbingPoints.Length; i++)
            {
                if (activeGrabbingPoint != grabbingPoints[i])
                    grabbingPoints[i].transform.position = new Vector3(startPositions[i].x, Mathf.Min(startPositions[i].y, startPositions[i].y + scaleChange), startPositions[i].z);
            }
            // Calculate the velocity based on the change in position
            releaseVelocity = (activeGrabbingPoint.transform.position - previousPosition) / Time.deltaTime;
            previousPosition = activeGrabbingPoint.transform.position;
        }
        else if (!isReturning)
        {
            // Start returning the pole to its original position
            release = StartCoroutine(ReturnPoleToOriginalPosition());
        }

        // Calculate the percentage of how much the pole has been pulled down
        float pulledPercentage = (pole.transform.localScale.y - baseLength) / sailOpenPoint;
        Debug.Log(1f - pulledPercentage);

        // Invert the percentage since the animation starts opened
        float invertedPercentage = 1f - pulledPercentage;

        // Set the sail's animation parameter
        sailAnimator.SetFloat("SailOpenAmount", invertedPercentage);
        sailAnimator.Play("SailClose", 0, sailAnimator.GetFloat("SailOpenAmount"));

    }

    private IEnumerator ReturnPoleToOriginalPosition()
    {
        isReturning = true;
        Debug.Log(releaseVelocity.y);
        while (releaseVelocity.y < -0.01f)
        {
            float distanceToMove = -(releaseVelocity.y*velocityDampMultiplier) * Time.deltaTime;
            pole.transform.localScale += new Vector3(0, distanceToMove, 0);

            for (int i = 0; i < grabbingPoints.Length; i++)
            {
                grabbingPoints[i].transform.position += new Vector3(0, -distanceToMove, 0);
            }

            releaseVelocity.y += velocityDecayRate * Time.deltaTime;

            yield return null;
        }
        while (Mathf.Abs(pole.transform.localScale.y - baseLength) > 0.01f) // Use a small threshold to prevent infinite loops
        {
            float distanceToMove = returningSpeed * Time.deltaTime;
            if (pole.transform.localScale.y > baseLength)
            {
                pole.transform.localScale -= new Vector3(0, distanceToMove, 0);
            }
            else
            {
                pole.transform.localScale += new Vector3(0, distanceToMove, 0);
            }

            // Adjust the position of all grabbing points based on the distance moved
            // Lerp the position of each grabbing point back to its original position
            for (int i = 0; i < grabbingPoints.Length; i++)
            {
                grabbingPoints[i].transform.position += new Vector3(0, distanceToMove, 0);
            }

            yield return null;
        }

        pole.transform.localScale = new Vector3(pole.transform.localScale.x, baseLength, pole.transform.localScale.z);
        for (int i = 0; i < grabbingPoints.Length; i++)
        {
            grabbingPoints[i].transform.position = startPositions[i];
        }

        isReturning = false;
    }

    // Called when a grabbing point is grabbed
    private void OnGrabbingPointGrabbed(SelectEnterEventArgs args)
    {
        activeGrabbingPoint = args.interactableObject as XRGrabInteractable;
        if (isReturning)
        {
            previousPosition = activeGrabbingPoint.transform.position;
            StopCoroutine(release); // Stop the coroutine if it's running
            isReturning = false;
        }
    }

    // Called when a grabbing point is released
    private void OnGrabbingPointReleased(SelectExitEventArgs args)
    {
        if (activeGrabbingPoint == (args.interactableObject as XRGrabInteractable))
        {        
            activeGrabbingPoint = null;
        }
    }
}
