using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CapSocket : XRSocketInteractor
{  
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        args.interactableObject.transform.localScale = Vector3.one;
        base.OnSelectEntering(args);
    }
    
}
