using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPickup2 : IsState
{
    public void OnEnter(StateController sc) {
        InnerOnEnter((RoomDay2SceneManager)sc);
    }

    void InnerOnEnter(RoomDay2SceneManager sc) {
        sc.waterDrinkDialogue.PlayDialogue(sc, 2, true);
    }

    public void OnExit(StateController sc) {
        InnerOnExit((RoomDay2SceneManager)sc);
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((RoomDay2SceneManager)sc);
    }

    void InnerOnExit(RoomDay2SceneManager sc) {
        sc.actionMoveProvider.enabled = true;
        sc.actionTurnProvider.enabled = true;
        sc.avatarAnimator.SetBool("drankWater", true);
        sc.player.GetComponent<Transform>().position = sc.getUpPosition.position;
    }

    void InnerUpdateState(RoomDay2SceneManager sc) {
        if (sc.PlayerDrank)
        {
            sc.ChangeState(sc.roam);

        }
        // change state
    }
}
