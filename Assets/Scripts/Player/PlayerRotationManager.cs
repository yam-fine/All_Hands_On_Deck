using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerRotationManager : MonoBehaviour
{

    Vector3 direction;
    public Transform xrorigin;
    public Transform camera;
    public Transform avatar;

    void Start() {
    }

    void Update() {
        // InputDevice handR = InputDevices.GetDeviceAtXRNode(XRNode.Head);
        // handR.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotR);
        // direction = rotR * Vector3.forward;
        
        avatar.rotation = xrorigin.rotation;
        
    }

    
    
}
