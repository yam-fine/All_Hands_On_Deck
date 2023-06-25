using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helm : MonoBehaviour
{
    [SerializeField]
    float rotationtime = 2;
    Coroutine cr;

    public void RotateHelmToZero() {
        cr = StartCoroutine(RotateHelmCoroutine());
    }

    public void SelectHelm(){
        if (cr != null) {
            StopCoroutine(cr);
            cr = null;
        }
    }

    private IEnumerator RotateHelmCoroutine()
    {
        float elapsedTime = 0f; // Time elapsed since starting the rotation
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < rotationtime)
        {
            // Interpolate the rotation between the start and target rotations based on time
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, elapsedTime / rotationtime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the wheel's rotation is exactly at the target rotation
        transform.rotation = Quaternion.identity;
    }
}
