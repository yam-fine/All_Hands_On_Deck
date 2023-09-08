using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder2 : IsState
{
    bool playerClimbed = false;
    bool got_hook = false;
    float stormWaveHeight = 5;
    float stormWaveFreq = 2;

    public void OnEnter(StateController sc) {
        InnerOnEnter((ShipDay2)sc);
    }

    void InnerOnEnter(ShipDay2 sc) {
        sc.enviro.configuration = sc.configs[1];
        sc.enviro.Weather.ChangeWeather("Foggy");
    }

    public void OnExit(StateController sc) {
        InnerOnExit((ShipDay2)sc);
    }

    void InnerOnExit(ShipDay2 sc) {
        sc.enviro.configuration = sc.configs[2];
        sc.enviro.Weather.ChangeWeather("Cloudy 2");

        sc.player_climb.should_apply_gravity = true;
        sc.hook_walls.SetActive(false);
        sc.hookExplanation.gameObject.SetActive(false);
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2)sc);
    }

    void InnerUpdateState(ShipDay2 sc) {
        if (sc.hook_pile.playerReached && !got_hook) {
            Debug.Log("hook");
            sc.hookExplanation.gameObject.SetActive(true);
            got_hook = true;
            sc.hook_walls.SetActive(true);
            sc.hook.SetActive(true);
            sc.rope.SetActive(true);
        }

        if (sc.hook_socket.hasSelection) {
            sc.hook_socket.GetComponentInChildren<MeshRenderer>().enabled = false;
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

            // change state
            sc.ChangeState(sc.final);
            //teleport to hull
            //sc.TeleportWithFade(sc.ChangeState, sc.hull);
        }
    }
}
