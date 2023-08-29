using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class WheelTurn : MonoBehaviour
{
    public List<XRGrabInteractable> handles;
    private XRBaseInteractor holdingHand;
    private XRGrabInteractable currentHnalde;
    private Vector3 lastPosition;
    private Coroutine release;

    public Transform Ship;

    [SerializeField] float speed = 50;



    private bool IsReturning;

    private void Awake()
    {
        for (int i = 0; i < handles.Count; i++)
        {
            handles[i].selectEntered.AddListener(OnGrab);
            handles[i].selectExited.AddListener(OnRelease);

        }
        
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (IsReturning)
        {
            StopCoroutine(release);
            IsReturning = false;

        }
        currentHnalde = args.interactableObject as XRGrabInteractable;
        lastPosition = transform.InverseTransformPoint(currentHnalde.transform.position);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if(currentHnalde == args.interactableObject as XRGrabInteractable)
        {
            currentHnalde = null;
            //transform.rotation = Quaternion.LookRotation(Ship.forward, Vector3.up);
        }
        
    }
    
    private void Update()
    {
        if(currentHnalde != null)
        {
            var currentHandlePosition = transform.InverseTransformPoint(currentHnalde.transform.position);
            var toLastPostion = lastPosition;// vector from last position to wheel center
            var toCurrentPosition = currentHandlePosition; // vector current position to wheel center 

            float angle = Vector3.SignedAngle(toLastPostion, toCurrentPosition, transform.right);

            transform.Rotate(transform.right, angle);
         
            lastPosition = currentHandlePosition;
        }

        else if (!IsReturning)
        {
            release = StartCoroutine(Return());
        }
    }

    private IEnumerator Return()
    {
        IsReturning = true;
        while (Mathf.Abs(transform.rotation.x - Quaternion.identity.x) >= 0.1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime*speed);
            Debug.Log(transform.rotation);
            yield return new WaitForEndOfFrame();
        }
        IsReturning = false;
    }
}
