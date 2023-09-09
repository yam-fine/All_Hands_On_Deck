using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhaleDayTwo : IsState
{

    private ShipDay2StateController state;

    public void OnEnter(StateController sc) {
        state = (ShipDay2StateController)sc;
        state.killerWhalePath.Attack();
        state.actionMoveProvider.enabled = false;
    }

    public void OnExit(StateController sc) {
        state = (ShipDay2StateController)sc;
        state.FadeFinish();
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2StateController)sc);
    }

    void InnerUpdateState(ShipDay2StateController sc) {
        
        if(sc.whaleShipColided.shipReached) {
            sc.audioManager.PlaySound(AudioManager.Sounds.big_wave, sc.avatar);
            sc.ChangeState(sc.cutscene);
        }
        
    }
}
