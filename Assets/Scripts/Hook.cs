using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hook : MonoBehaviour
{
    public InputActionAsset _actionAsset;
    InputAction activateAction;
    InputAction selectAction;
    public Transform playerHand; // Assign the player's hand transform here
    public float retrievalSpeed = 5f; // Speed at which the hook returns to the player's hand
    public bool isRetrieving = false;
    public bool isSelected = false;

    //public void Start() {
    //    activateAction = _actionAsset.FindActionMap("XRI RightHand").FindAction("Activate");
    //    activateAction.Enable();
    //    activateAction.started += OnActivateStarted;
    //    activateAction.canceled += OnActivateCanceled;
    //}

    private void OnEnable() {
        activateAction = _actionAsset.FindActionMap("XRI RightHand").FindAction("Activate");
        selectAction = _actionAsset.FindActionMap("XRI RightHand").FindAction("Select");
        activateAction.Enable();
        selectAction.Enable();
        activateAction.started += OnActivateStarted;
        activateAction.canceled += OnActivateCanceled;
        selectAction.started += OnSelectStarted;
        selectAction.canceled += OnSelectCanceled;
    }

    private void OnDisable() {

        activateAction.started -= OnActivateStarted;
        activateAction.canceled -= OnActivateCanceled;
    }

    private void OnActivateStarted(InputAction.CallbackContext context) {
        isRetrieving = true;
    }

    private void OnActivateCanceled(InputAction.CallbackContext context) {
        isRetrieving = false;
    }

    private void OnSelectStarted(InputAction.CallbackContext context) {
        isSelected = true;
    }

    private void OnSelectCanceled(InputAction.CallbackContext context) {
        isSelected = false;
    }

    private void Update() {
        if (isRetrieving && !isSelected) {
            transform.position = Vector3.MoveTowards(transform.position, playerHand.position, retrievalSpeed * Time.deltaTime);

            // Stop retrieving if the hook is close to the player's hand
            if (Vector3.Distance(transform.position, playerHand.position) < 0.1f) {
                isRetrieving = false;
            }
        }
    }
}
