using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneDayOne : IsState
{

    private ShipDay1StateController state;

    private bool playerReached = false;

    public void OnEnter(StateController sc) {
        state = (ShipDay1StateController)sc;
        state.actionMoveProvider.enabled = false;
        state.playerCutScenePath.Attack();
    }

    public void OnExit(StateController sc) {
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        
        if (sc.cutscene_desired_position.playerReached && !playerReached) {
            playerReached = true;
            sc.ChangeState(sc.finish);
        }
            
        
    }
}