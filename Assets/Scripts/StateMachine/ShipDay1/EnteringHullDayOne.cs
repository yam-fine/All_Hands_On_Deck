using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringHullDayOne : IsState
{
    bool event_reached = false;

    public void OnEnter(StateController sc) {
        InnerOnEnter((ShipDay1StateController)sc);
    }

    void InnerOnEnter(ShipDay1StateController sc) {

        foreach (GameObject go in sc.toDisapearOnHull) {
            go.SetActive(false);
        }
        //dialogue
        sc.beforeHullDialogue.PlayDialogue(sc, 2, true);

        sc.actionMoveProvider.enabled = true;
    }

    public void OnExit(StateController sc) {
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        if (sc.hull_desired_position.playerReached && !event_reached) {
            event_reached = true;
            sc.ChangeState(sc.hull);
        }
    }
}
