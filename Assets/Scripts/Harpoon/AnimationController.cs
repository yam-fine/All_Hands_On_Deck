using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class AnimationController : XRBaseInteractable
{
    public Animator animator;
    public Rigidbody rb;

    // protected override void OnSelectEntered(SelectEnterEventArgs args) {
        
    //     XRBaseInteractor interactor = args.interactor;
    //     base.OnSelectEntered(args);

    //     if (interactor is XRDirectInteractor) {
    //         animator.SetBool("Holding", true);
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Holding() {
        Debug.Log("Holding");
        // animator.SetBool("Holding", true);
    }
    
    public void Fire() {
        Debug.Log("Fire");
        // animator.SetBool("Fire", true);
    }

    public void ResetVelocity(){
        rb.angularVelocity = Vector3.zero;
    }


}
