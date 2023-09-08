using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class WheelTurn : MonoBehaviour
{
    public List<XRGrabInteractable> handles;
    public GameObject player;
    private XRBaseInteractor holdingHand;
    private XRGrabInteractable currentHnalde;
    private Vector3 lastPosition;
    private Coroutine release;

    public Transform Ship;

    [SerializeField] float speed = 50;
    [SerializeField] private List<Transform> startingPos;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject wheel;



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
        player.transform.SetParent(Ship);
        currentHnalde = args.interactableObject as XRGrabInteractable;
        lastPosition = currentHnalde.transform.position;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        player.transform.SetParent(null);
        var currentIntercator = args.interactableObject as XRGrabInteractable;
        if (currentHnalde == currentIntercator)
        {
            for (int i = 0; i < handles.Count; i++)
            {
                handles[i].transform.position = startingPos[i].position;
            }
            //currentHnalde.transform.position = startingPos[handles.IndexOf(currentHnalde)].position;
            currentHnalde = null;
            //transform.rotation = Quaternion.LookRotation(Ship.forward, Vector3.up);
        }
        
    }
    
    private void PlaySoundEffectIfNeeded() {
        if (audioManager) {
            if (!audioManager.IsPlaying(AudioManager.Sounds.wheel_string_fx)) {
                audioManager.PlaySound(AudioManager.Sounds.wheel_string_fx, wheel);
            }
        }
    }

    private void Update()
    {
        if(currentHnalde != null)
        {
            var currentHandlePosition = Ship.InverseTransformPoint(currentHnalde.transform.position);
            var toLastPostion = Ship.InverseTransformPoint(lastPosition) - Ship.InverseTransformPoint(transform.position); // vector from last position to wheel center
            var toCurrentPosition = currentHandlePosition - Ship.InverseTransformPoint(transform.position); // vector current position to wheel center 

            float angle = Vector3.SignedAngle(toLastPostion, toCurrentPosition, Vector3.right);

            transform.Rotate(Vector3.right, angle);

            PlaySoundEffectIfNeeded();

         
            lastPosition = Ship.TransformPoint(currentHandlePosition);
            player.transform.SetParent(Ship);
        }

        else if (!IsReturning)
        {
            release = StartCoroutine(Return());
        }
    }

    private IEnumerator Return()
    {
        IsReturning = true;

        // Calculate the angle difference between the wheel's forward and the Ship's forward
        float angleDiff = Vector3.Angle(transform.forward, Ship.forward);

        // While the angle difference is larger than a small threshold, adjust the wheel's rotation
        while (angleDiff > 0.1f)
        {
            // Calculate the rotation needed to align the wheel's forward with the Ship's forward
            Quaternion targetRotation = Quaternion.LookRotation(Ship.forward, Vector3.up); // Assuming the up vector is the world's up. Adjust if needed.

            // Rotate the wheel towards the target rotation by a fixed step
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed);

            PlaySoundEffectIfNeeded();

            // Recalculate the angle difference for the next loop iteration
            angleDiff = Vector3.Angle(transform.forward, Ship.forward);

            yield return new WaitForEndOfFrame();
        }

        IsReturning = false;
    }
}
