using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSteering : IsState
{
    float rotationThreshold = 1;

    public void OnEnter(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        if (Mathf.Abs(sc.ship.transform.rotation.y - sc.desiredRotation) <= rotationThreshold {
            sc.ChangeState(sc.ladder_climb);
        }
    }
}
