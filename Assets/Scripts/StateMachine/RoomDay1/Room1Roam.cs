using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Roam : IsState {
    public void OnEnter(StateController sc) {
        // dialouge 
        AudioManager am = AudioManager.Instance;
        am.PlayOneShotAttach(AudioManager.Sounds.ay_cap, sc.player);
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        // if door handle gets pulled then load next scene
    }
}
