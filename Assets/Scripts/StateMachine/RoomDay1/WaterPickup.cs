using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : IsState
{
    public void OnEnter(StateController sc) {
        // character sits on bed

        //temp
        //sc.ChangeState(sc.room1Roam);
    }

    public void OnExit(StateController sc) {
        sc.actionMoveProvider.enabled = true;
        sc.actionTurnProvider.enabled = true;
        sc.avatarAnimator.SetBool("drankWater", true);
    }

    public void UpdateState(StateController sc) {
        if (sc.PlayerDrank)
        {
            sc.ChangeState(sc.room1Roam);

        }
        // change state
    }
}
