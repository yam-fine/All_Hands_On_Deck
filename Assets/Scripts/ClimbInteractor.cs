using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractor : XRBaseInteractable
{
    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor) {
        base.OnSelectEntered(interactor);

        if (interactor is XRDirectInteractor)
            Climb.climbingHand = interactor.GetComponent<XRController>();
    }

    [System.Obsolete]
    protected override void OnSelectExited(XRBaseInteractor interactor) {
        base.OnSelectEntered(interactor);

        if (interactor is XRDirectInteractor) {
            if (Climb.climbingHand && Climb.climbingHand.name == interactor.name) {
                Climb.climbingHand = null;
            }
        }
    }
}
