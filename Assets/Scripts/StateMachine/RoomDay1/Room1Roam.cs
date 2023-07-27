using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Roam : IsState {
    public void OnEnter(StateController sc) {
        // dialouge 
        sc.roamDialogue.PlayDialogue(sc);
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        // immersion dialogue
    }
}
