using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : IsState
{
    bool playerClimbed = false;
    float stormWaveHeight = 5;
    float stormWaveFreq = 2;

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

            sc.whale.SetActive(true);
            sc.waterMat.SetFloat("_WaveScale", stormWaveHeight);
            sc.waterMat.SetFloat("_WaveFrequency", stormWaveFreq);

            //weather change
            sc.enviro.Weather.ChangeWeather("Snow");
            
            sc.actionMoveProvider.enabled = false;

            //teleport to hull
            sc.TeleportWithFade(sc.ChangeState, sc.hull);
        }
    }
}
