using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class AnimationController : XRBaseInteractable
{
    public Animator animator;
    public Rigidbody rb;

    [System.Obsolete]
    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        Debug.Log("ENTERED");
        rb.freezeRotation = false;
        XRBaseInteractor interactor = args.interactor;
        base.OnSelectEntered(args);

        if (interactor is XRDirectInteractor) {
            //Climb.climbingHand = interactor.GetComponent<ActionBasedController>()
            animator.SetBool("Holding", true);
        }
    }

    // protected override void OnSelectExited(SelectExitEventArgs args) {
    //     Debug.Log("EXITED");
    //     rb.freezeRotation = true;
    //     //rb.velocity = Vector3.zero;
    // }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ResetVelocity(){
        rb.angularVelocity = Vector3.zero;
        //rb.velocity = 0;
    }


}
