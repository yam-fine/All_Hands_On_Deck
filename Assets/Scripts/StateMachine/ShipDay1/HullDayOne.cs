using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullDayOne : IsState
{
    bool event_reached = false;

    public void OnEnter(StateController sc) {
        InnerOnEnter((ShipDay1StateController)sc);
    }

    void InnerOnEnter(ShipDay1StateController sc) {
        //dialogue
        sc.actionMoveProvider.enabled = true;
        sc.givingCPR.StartCPR();
        sc.takingCPR.StartGettingCPR();
    }

    public void OnExit(StateController sc) {
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        if (sc.whale_desired_position.playerReached && !event_reached) {
            event_reached = true;
            Debug.Log("STATE-TRANSITION");
            sc.ChangeState(sc.whaleState);
        }
    }
}
