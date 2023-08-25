using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelTurn : MonoBehaviour
{
    public List<XRGrabInteractable> handles;
    private XRBaseInteractor holdingHand;
    private Vector3 lastPosition;

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
        holdingHand = args.interactorObject as XRBaseInteractor;
        lastPosition = holdingHand.transform.position;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        holdingHand = null;
    }

    private void Update()
    {
        if(holdingHand != null)
        {
            var currentHandlePosition = holdingHand.transform.position;
            var toLastPostion = lastPosition - transform.position; // vector from last position to wheel center
            var toCurrentPosition = currentHandlePosition - transform.position; // vector current position to wheel center 

            float angle = Vector3.SignedAngle(toLastPostion, toCurrentPosition, Vector3.right);

            transform.Rotate(Vector3.right, angle, Space.World);
        }
    }
}
