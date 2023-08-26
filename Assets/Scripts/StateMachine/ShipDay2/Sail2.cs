using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail2 : IsState {
    public void OnEnter(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2)sc);
    }

    void InnerUpdateState(ShipDay2 sc) {
        if (RopePullingInteractor.stop) {
            sc.ChangeState(sc.wheel_steering);
        }
    }
}
