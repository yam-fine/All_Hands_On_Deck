using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer2 : IsState
{
    float rotationThreshold = 1;


    private void InnerEnterState(ShipDay2StateController sc) {
        sc.wheelSteerDialogue.PlayDialogue(sc, 5, true);
    }

    public void OnEnter(StateController sc) {
        InnerEnterState((ShipDay2StateController)sc);
    }

    

    public void OnExit(StateController sc) {
        // throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2StateController)sc);
    }

    void InnerUpdateState(ShipDay2StateController sc) {
        if (Mathf.Abs(sc.ship.transform.localRotation.eulerAngles.y - sc.desiredRotation) <= rotationThreshold) {
            sc.ChangeState(sc.ladder_climb);
        }
    }
}
