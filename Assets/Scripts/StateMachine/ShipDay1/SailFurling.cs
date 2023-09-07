using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailFurling : IsState
{

    private int nextUpdate = 0;


    public void OnEnter(StateController sc) {
        InnerEnterState((ShipDay1StateController)sc);
        
    }

    public void InnerExitState(ShipDay1StateController sc) {
        sc.sailsNPC.StopWaving();
        sc.sailsUpDialogue.PlayDialogue(sc);
    }

    public void OnExit(StateController sc) {
        InnerExitState((ShipDay1StateController)sc);
    }
    

    

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {

        // dialogue
        if(Time.time >= nextUpdate){
    		nextUpdate = Mathf.FloorToInt(Time.time)+30;
    		sc.audioManager.PlaySound(AudioManager.Sounds.ay_leut, GameObject.Find("Will (7)"));
    	}

        if (RopePullingInteractor.stop) {
            sc.ChangeState(sc.wheel_steering);
        }
    }


    void InnerEnterState(ShipDay1StateController sc) {
        sc.sailsDownDialogue.PlayDialogue(sc);
    }
}
