using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInterfaceable : XRGrabInteractable
{
    private Vector3 ininitialAttachLocalPos;
    private Quaternion initialAttachLocalRot;
    void Start()
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        ininitialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
            
        }
        else
        {
            attachTransform.localPosition = ininitialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }
        base.OnSelectEntered(args);
    }
}
