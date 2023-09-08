using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : IsState {
    public void OnEnter(StateController sc) {
        InnerOnEnter((ShipDay2)sc);
    }

    void InnerOnEnter(ShipDay2 sc) {
        sc.fade.FadeOut();
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        InnerUpdate((ShipDay2)sc);
    }

    private void InnerUpdate(ShipDay2 sc) {
        float timer = 0;
        Debug.Log("hi");
        while (timer < sc.fade.fadeDur) {
            timer += Time.deltaTime;
            return;
        }
        Debug.Log("hiii");
        sc.player.transform.position = sc.teleportAfterLadder.position;
        sc.player.transform.rotation = sc.teleportAfterLadder.rotation;
        sc.fade.FadeIn();
    }
}
