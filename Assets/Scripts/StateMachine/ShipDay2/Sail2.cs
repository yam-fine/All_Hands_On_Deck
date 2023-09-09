using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail2 : IsState {

    private int nextUpdate = 0;

    public bool pulled = false;

    public void OnEnter(StateController sc) {
    }

    public void InnerExitState(ShipDay2StateController sc) {
        sc.sailsNPC.StopWaving();
        sc.sailsDownDialogue.PlayDialogue(sc, 3);
    }

    public void OnExit(StateController sc) {
        InnerExitState((ShipDay2StateController)sc);
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2StateController)sc);
    }

    void InnerUpdateState(ShipDay2StateController sc) {

        // dialogue
        if(Time.time >= nextUpdate && !pulled){
    		nextUpdate = Mathf.FloorToInt(Time.time) + 30; // play every 30 seconds
    		sc.sailsUpDialogue.PlayDialogue(sc, 3);
    	}

        if(sc.ropePullingInteractor.IsPulling() && !pulled) {
            pulled = true;
        }

        if (pulled) {
            sc.sailsUpDialogue.StopDialogue();
        }


        if (RopePullingInteractor.stop) {
            sc.ChangeState(sc.wheel_steering);
        }
    }
}
