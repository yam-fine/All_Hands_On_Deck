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
    
    [SerializeField] private GameObject pole; // Reference to the pole
    [SerializeField] private List<XRGrabInteractable> grabbingPoints = new List<XRGrabInteractable>(); // List of grabbing points

    private List<Vector3> startPositions = new List<Vector3>(); // Store the start positions of each grabbing point
    private float baseLength;
    private bool isReturning = false;
    [SerializeField] private XRGrabInteractable activeGrabbingPoint = null;
    private float currentLength;
    private Coroutine release;

    private Vector3 previousPosition;
    private Vector3 releaseVelocity = Vector3.zero;
    public static bool stop;
    [SerializeField] private float velocityDampMultiplier = 0.5f;
    [SerializeField] private float velocityDecayRate = 30f;
    [SerializeField] private float returningSpeed = 4.0f;
    [SerializeField] private Animator sailAnimator;
    [SerializeField] private float sailOpenPoint;

    public AudioManager audioManager;

    public void AddGrabingPoint(XRGrabInteractable grabbingPoint, Vector3 startingPosition)
    {
        grabbingPoints.Add(grabbingPoint);
        startPositions.Add(startingPosition);

        int i = grabbingPoints.Count - 1;
        grabbingPoint.selectEntered.AddListener(OnGrabbingPointGrabbed);
        grabbingPoint.selectExited.AddListener(OnGrabbingPointReleased);
 
    }

    public void RemoveGrabbingPoint(XRGrabInteractable grabbingPoint)
    {
        int index = grabbingPoints.IndexOf(grabbingPoint);
        grabbingPoints.Remove(grabbingPoint);
        startPositions.RemoveAt(index);
        grabbingPoint.selectEntered.RemoveListener(OnGrabbingPointGrabbed);
        grabbingPoint.selectExited.RemoveListener(OnGrabbingPointReleased);

    }

    private void Awake() {
        stop = false;
    }
    private void Start()
    {
        baseLength = pole.transform.localScale.y;
        currentLength = baseLength;

        // Initialize the start positions array
        for (int i = 0; i < grabbingPoints.Count; i++)
        {
            startPositions.Add(grabbingPoints[i].transform.position);

            // Add event listeners for the SelectEnter and SelectExit events
            grabbingPoints[i].selectEntered.AddListener(OnGrabbingPointGrabbed);
            grabbingPoints[i].selectExited.AddListener(OnGrabbingPointReleased);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < grabbingPoints.Count; i++)
        {
            
            grabbingPoints[i].selectEntered.RemoveListener(OnGrabbingPointGrabbed);
            grabbingPoints[i].selectExited.RemoveListener(OnGrabbingPointReleased);
        }
    }

    void Update()
    {
        if (stop)
            return;
        if (activeGrabbingPoint != null)
        {
            // Scale the pole based on the active grabbing point's position
            float scaleChange = (activeGrabbingPoint.transform.position - startPositions[grabbingPoints.IndexOf(activeGrabbingPoint)]).y;
            currentLength = Mathf.Max(baseLength, baseLength - scaleChange);
            pole.transform.localScale = new Vector3(pole.transform.localScale.x, currentLength, pole.transform.localScale.z);

            // Adjust the position of all grabbing points based on the scale change
            for (int i = 0; i < grabbingPoints.Count; i++)
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
        // Invert the percentage since the animation starts opened
        float invertedPercentage = 1f - pulledPercentage;

        // Set the sail's animation parameter
        sailAnimator.SetFloat("SailOpenAmount", invertedPercentage);
        sailAnimator.Play("SailClose", 0, sailAnimator.GetFloat("SailOpenAmount"));
        if (sailAnimator.GetFloat("SailOpenAmount") <= 0)
        {
            stop = true;
        }

    }

    private IEnumerator ReturnPoleToOriginalPosition()
    {
        isReturning = true;
        while (releaseVelocity.y < -0.01f)
        {
            float distanceToMove = -(releaseVelocity.y*velocityDampMultiplier) * Time.deltaTime;
            pole.transform.localScale += new Vector3(0, distanceToMove, 0);

            for (int i = 0; i < grabbingPoints.Count; i++)
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
            for (int i = 0; i < grabbingPoints.Count; i++)
            {
                grabbingPoints[i].transform.position += new Vector3(0, distanceToMove, 0);
            }

            yield return null;
        }

        pole.transform.localScale = new Vector3(pole.transform.localScale.x, baseLength, pole.transform.localScale.z);
        for (int i = 0; i < grabbingPoints.Count; i++)
        {
            grabbingPoints[i].transform.position = startPositions[i];
        }

        isReturning = false;
    }

    // Called when a grabbing point is grabbed
    private void OnGrabbingPointGrabbed(SelectEnterEventArgs args)
    {

        if (audioManager) {audioManager.PlaySound(AudioManager.Sounds.rope_pull_fx);}
        

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

    public bool IsPulling() { return activeGrabbingPoint != null; }
}
