using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class AvatarAnimationController : MonoBehaviour
{
    [SerializeField] private InputActionReference move;

    [SerializeField] private Animator animator;

    public AudioManager am;

    // do not change nextUpdate to float, must be double
    private double nextUpdate = 0.5;

    private void OnEnable() {
        move.action.started += AnimateLegs;
        move.action.canceled += StopAnimateLegs;
    }

    private void OnDisable() {

        move.action.started -= AnimateLegs;
        move.action.canceled -= StopAnimateLegs;

    }
    
    void Update() {
        if(Time.time>=nextUpdate){
    		nextUpdate=Time.time+0.5;
    		if (animator.GetBool("isWalking")) {
                am.PlaySound(AudioManager.Sounds.steps);
            }
    	}
        
    }


    private void AnimateLegs(InputAction.CallbackContext context)
    {
        // bool isMovingForward = move.action.ReadValue<Vector2>().y > 0;
        animator.SetBool("isWalking", true);
    }

    private void StopAnimateLegs(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", false);
        
    }
}
