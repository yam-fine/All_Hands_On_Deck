using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationManager : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private InputActionReference activateReference;

    [SerializeField]
    private InputActionReference selectReference;

    private InputAction activateAction;
    private InputAction selectAction;

    private void Awake() {
        animator = GetComponent<Animator>();
        activateAction = activateReference.action;
        selectAction = selectReference.action;

        // if (animator != null && animator.isActiveAndEnabled)
        // {
        //     animator.Play("Blend Tree");
        // }
    }


    private void Update() {
        animator.SetFloat("Activate", activateAction.ReadValue<float>());
        animator.SetFloat("Select", selectAction.ReadValue<float>());
    }

}
