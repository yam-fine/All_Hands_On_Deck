using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    HingeJoint hinge;
    Transform tform;

    bool fireEvent = true;

    public void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        tform = GetComponent<Transform>();
    }

    public void Update()
    {
        if (tform.rotation.eulerAngles.z >= 35)
        {
            if (fireEvent)
            {
                fireEvent = false;
                Action();
            }
        }
        else if (tform.rotation.eulerAngles.z <= 5)
        {
            fireEvent = true;
        }
    }

    private void Action()
    {
        Debug.Log("The door's lever has been pulled");
    }
}
