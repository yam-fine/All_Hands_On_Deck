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
        }
        else if (!isReturning)
        {
            // Start returning the pole to its original position
            StartCoroutine(ReturnPoleToOriginalPosition());
        }
    }

    private IEnumerator ReturnPoleToOriginalPosition()
    {
        isReturning = true;

        while (Mathf.Abs(pole.transform.localScale.y - baseLength) > 0.01f) // Use a small threshold to prevent infinite loops
        {
            float distanceToMove = 0.5f * Time.deltaTime;
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
            StopCoroutine(ReturnPoleToOriginalPosition()); // Stop the coroutine if it's running
            isReturning = false;
        }
    }

    // Called when a grabbing point is released
    private void OnGrabbingPointReleased(SelectExitEventArgs args)
    {
        if (activeGrabbingPoint == (args.interactableObject as XRGrabInteractable))
        {
            Rigidbody rb = activeGrabbingPoint.GetComponent<Rigidbody>();
            Vector3 currentVelocity = rb.velocity;
            Debug.Log(currentVelocity);
            activeGrabbingPoint = null;
        }
    }
}
