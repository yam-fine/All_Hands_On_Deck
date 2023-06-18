using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climb : MonoBehaviour
{
    //Rigidbody rb;
    CharacterController cc;
    LocomotionSystem ls;
    public static ActionBasedController climbingHand;

    private ActionBasedController previousHand;
    private Vector3 previousPos;
    private Vector3 velocity;

    private void Start() {
        //rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        ls = GetComponent<LocomotionSystem>();
    }

    //private void FixedUpdate() {
    //    if (climbingHand) {
    //        ls.enabled = false;
    //        Character_Climb();
    //    }
    //    else {
    //        ls.enabled = true;
    //    }
    //}


    void Update() {

        if (climbingHand) {
            if (previousHand == null) {
                previousHand = climbingHand;
                previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
            }
            if (climbingHand.name != previousHand.name) {
                previousHand = climbingHand;
                previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
                //Debug.Log("DIFFERENT HAND NOW");
            }
            ls.enabled = false;
            Character_Climb();
        }
        else {
            ls.enabled = true;
            ApplyGravity();
            previousHand = null;
        }
    }


    private void Character_Climb() {
        Debug.Log("climb climb");
        //InputDevices.GetDeviceAtXRNode(climbingHand).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        //Debug.Log(velocity);
        Vector3 velocity = (climbingHand.positionAction.action.ReadValue<Vector3>() - previousPos) / Time.deltaTime;
        cc.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
        previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
    }

    private void ApplyGravity() {
        if (!cc.isGrounded) {
            cc.Move(Vector3.down * 5 * Time.deltaTime);
        }
    }
}
