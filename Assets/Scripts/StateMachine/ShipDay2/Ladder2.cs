using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder2 : IsState
{
    bool playerClimbed = false;
    float stormWaveHeight = 5;
    float stormWaveFreq = 2;

    public void OnEnter(StateController sc) {
        InnerOnEnter((ShipDay2)sc);
    }

    void InnerOnEnter(ShipDay2 sc) {
        sc.hook_walls.SetActive(true);
        foreach (GameObject obj in sc.ropeComps) {
            obj.SetActive(true);
        }
    }

    public void OnExit(StateController sc) {
        InnerOnExit((ShipDay2)sc);
    }

    void InnerOnExit(ShipDay2 sc) {
        sc.player_climb.should_apply_gravity = true;
        sc.hook_walls.SetActive(false);
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2)sc);
    }

    void InnerUpdateState(ShipDay2 sc) {
        if (sc.hook_socket.hasSelection) {
            sc.player_climb.should_apply_gravity = false;
        }
        else {
            sc.player_climb.should_apply_gravity = true;
        }
        if (sc.ld.playerReached && !playerClimbed) {
            playerClimbed = true;

            sc.whale.SetActive(true);
            sc.waterMat.SetFloat("_WaveScale", stormWaveHeight);
            sc.waterMat.SetFloat("_WaveFrequency", stormWaveFreq);

            //weather change
            sc.enviro.Weather.ChangeWeather("Snow");

            sc.actionMoveProvider.enabled = false;

            //teleport to hull
            //sc.TeleportWithFade(sc.ChangeState, sc.hull);
        }
    }
}
