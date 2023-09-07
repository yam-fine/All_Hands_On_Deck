using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup : IsState
{
    public void OnEnter(StateController sc) {
        InnerOnEnter((RoomDay1StateController)sc);
    }

    void InnerOnEnter(RoomDay1StateController sc) {
        sc.waterDrinkDialogue.PlayDialogue(sc);
    }

    public void OnExit(StateController sc) {
        InnerOnExit((RoomDay1StateController)sc);
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((RoomDay1StateController)sc);
    }

    void InnerOnExit(RoomDay1StateController sc) {
        sc.actionMoveProvider.enabled = true;
        sc.actionTurnProvider.enabled = true;
        sc.avatarAnimator.SetBool("drankWater", true);
        sc.player.GetComponent<Transform>().position = sc.getUpPosition.position;
    }

    void InnerUpdateState(RoomDay1StateController sc) {
        if (sc.PlayerDrank)
        {
            sc.ChangeState(sc.room1Roam);

        }
        // change state
    }
}
