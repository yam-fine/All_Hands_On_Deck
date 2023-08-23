using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDayOne : IsState
{

    private ShipDay1StateController sdsc;
    public void OnEnter(StateController sc) {
        sdsc = (ShipDay1StateController)sc;
        sdsc.fade.FadeOut();
        sdsc.LoadScene();
    }

    public void OnExit(StateController sc) {
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        
    }
}
