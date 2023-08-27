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
        foreach (GameObject obj in sc.ropeComps) {
            obj.SetActive(true);
        }
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay2)sc);
    }

    void InnerUpdateState(ShipDay2 sc) {
        if (sc.hook_socket.hasSelection) {
            sc.player.GetComponent<Rigidbody>().useGravity = false;
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
