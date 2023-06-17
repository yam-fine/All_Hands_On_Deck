using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractor : XRBaseInteractable
{
    [System.Obsolete]
    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        XRBaseInteractor interactor = args.interactor;
        base.OnSelectEntered(args);
        if (interactor is XRDirectInteractor) {
            Debug.Log("HELLO");
            Climb.climbingHand = interactor.GetComponent<ActionBasedController>();
            Debug.Log(Climb.climbingHand);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args) {
        base.OnSelectExited(args);

        //if (interactor is XRDirectInteractor) {
        //    if (Climb.climbingHand && Climb.climbingHand.name == interactor.name) {
        //        Climb.climbingHand = null;
        //    }
        //}

        if (Climb.climbingHand && Climb.climbingHand.name == args.interactorObject.transform.name) {
            Climb.climbingHand = null;
        }

    }
}
