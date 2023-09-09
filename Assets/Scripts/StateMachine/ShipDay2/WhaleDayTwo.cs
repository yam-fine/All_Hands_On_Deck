using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhaleDayTwo : IsState
{

    private ShipDay1StateController state;

    public void OnEnter(StateController sc) {
        state = (ShipDay1StateController)sc;
        state.killerWhalePath.Attack();
    }

    public void OnExit(StateController sc) {
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        
        if(sc.whaleShipColided.shipReached) {
            sc.audioManager.PlaySound(AudioManager.Sounds.big_wave, GameObject.Find("Waypoint (2)-cs"));
            sc.ChangeState(sc.cutscene);
        }
        
    }
}
