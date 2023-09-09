using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneDayTwo : IsState
{

    private ShipDay2StateController state;

    private bool playerReached = false;

    public void OnEnter(StateController sc) {
        state = (ShipDay2StateController)sc;
        state.actionMoveProvider.enabled = false;
        state.captainCutScenePath.Attack();
        state.capAnimator.SetBool("IsFalling", true);
    }

    public void OnExit(StateController sc) {
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2StateController)sc);
    }

    void InnerUpdateState(ShipDay2StateController sc) {
        
        if (sc.cutscene_desired_position.playerReached && !playerReached) {
            playerReached = true;
            sc.ChangeState(sc.finish);
        }
            
        
    }
}