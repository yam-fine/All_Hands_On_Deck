using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : IsState
{
    bool playerClimbed = false;
    float stormWaveHeight = 5;
    float stormWaveFreq = 2;

    public void OnEnter(StateController sc) {
        InnerOnEnter((ShipDay1StateController)sc);
    }

    void InnerOnEnter(ShipDay1StateController sc) {
        sc.enviro.configuration = sc.configs[1];
        sc.enviro.Weather.ChangeWeather("Foggy");
    }

    public void OnExit(StateController sc) {
        InnerOnExit((ShipDay1StateController)sc);
    }

    void InnerOnExit(ShipDay1StateController sc) {
        sc.enviro.configuration = sc.configs[2];
        sc.enviro.Weather.ChangeWeather("Cloudy 2");
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        if (sc.ld.playerReached && !playerClimbed) {
            playerClimbed = true;

            sc.whale.SetActive(true);
            sc.waterMat.SetFloat("_WaveScale", stormWaveHeight);
            sc.waterMat.SetFloat("_WaveFrequency", stormWaveFreq);

            //weather change
            //sc.enviro.configuration = sc.configs[2];
            //sc.enviro.Weather.ChangeWeather("Cloudy 2");

            sc.actionMoveProvider.enabled = false;

            //teleport to hull
            sc.TeleportWithFade(sc.ChangeState, sc.hull);
        }
    }
}
