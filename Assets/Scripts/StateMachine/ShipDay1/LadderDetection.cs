using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetection : MonoBehaviour
{
    [HideInInspector] public bool playerReached = false;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "XR Origin")
            Debug.Log("hi");
            playerReached = true;
    }
}
