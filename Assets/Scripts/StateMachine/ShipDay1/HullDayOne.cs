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
        sc.givingCPR.StartCPR();
        sc.takingCPR.StartGettingCPR();

        //dialogue
        sc.hullDialogue.PlayDialogue(sc, 2, true);

        // sound effects

        sc.musicControl.SetHallParam(true);


        
    }

    public void InnerExitState(ShipDay1StateController sc) {
        sc.musicControl.SetHallParam(false);
    }

    public void OnExit(StateController sc) {
        InnerExitState((ShipDay1StateController)sc);
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        if (sc.whale_desired_position.playerReached && !event_reached) {
            event_reached = true;
            sc.ChangeState(sc.whaleState);
        }
    }
}
