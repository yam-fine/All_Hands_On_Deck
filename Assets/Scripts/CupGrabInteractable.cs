using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CupGrabInteractable : XRGrabInteractable
{
    [SerializeField] private XRGrabInteractable bottle;
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (bottle != null && bottle.isSelected)
        {
            // Allow detachment if the bottle is being held
            base.OnSelectExited(args);
        }
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (bottle != null && bottle.isSelected)
        {
            // Allow detachment if the bottle is being held
            base.OnSelectEntering(args);
        }
    }
}
