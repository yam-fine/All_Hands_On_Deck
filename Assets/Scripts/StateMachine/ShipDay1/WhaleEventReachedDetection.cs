using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleEventReachedDetection : MonoBehaviour
{
    [HideInInspector] public bool shipReached = false;


    IEnumerator ExampleCoroutine()
    {
        // TODO: play relevant sound
        // TODO: fade out
        yield return new WaitForSeconds(1);
        shipReached = true;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.name == "KillerWhale") {
            StartCoroutine(ExampleCoroutine());
            // shipReached = true;
        }
            
    }
}
