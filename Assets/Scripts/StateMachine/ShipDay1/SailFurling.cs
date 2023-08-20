using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailFurling : IsState
{
    public void OnEnter(StateController sc) {
        // dialogue
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        if (RopePullingInteractor.stop) {
            sc.ChangeState(sc.wheel_steering);
        }
    }
}
