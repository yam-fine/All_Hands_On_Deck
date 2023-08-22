using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : IsState
{
    bool playerClimbed = false;

    public void OnEnter(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void OnExit(StateController sc) {
        // fade to black and teleport player to hull
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        if (sc.ld.playerReached && !playerClimbed) {
            playerClimbed = true;

            //weather change
            sc.enviro.Weather.ChangeWeather("Snow");

            //teleport to hull
            //sc.ChangeState(sc.hull_n_whale);
        }
    }
}
