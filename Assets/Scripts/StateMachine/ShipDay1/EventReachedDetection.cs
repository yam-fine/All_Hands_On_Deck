using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReachedDetection : MonoBehaviour
{
    [HideInInspector] public bool playerReached = false;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "XR Origin")
            playerReached = true;
    }
}
