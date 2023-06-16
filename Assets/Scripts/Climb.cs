using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Climb : MonoBehaviour
{
    CharacterController character;
    public static XRController climbingHand;

    private void Start() {
        character = GetComponent<CharacterController>();

    }

    private void Character_Climb() {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);
        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }
}
