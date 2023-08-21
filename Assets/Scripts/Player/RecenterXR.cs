using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecenterXR : MonoBehaviour
{
    public Transform head;
    public Transform origin;
    public Transform target;


    private void Start()
    {
        Recenter();
    }

    public void Recenter()
    {
        var rotationAngleY = target.rotation.eulerAngles.y - head.transform.rotation.eulerAngles.y;
        origin.transform.Rotate(0, rotationAngleY, 0);

        var distanceDiff = target.position - head.transform.position;
        origin.transform.position += distanceDiff;
    }
}
