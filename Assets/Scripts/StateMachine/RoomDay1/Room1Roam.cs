using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Roam : IsState {
    public void OnEnter(StateController sc) {
        // dialouge 
        InnerOnEnter((RoomDay1StateController)sc);
    }

    void InnerOnEnter(RoomDay1StateController sc) {
        sc.roamDialogue.PlayDialogue(sc, 2, true);
        sc.xrOrigin.GetComponent<CharacterController>().enabled = false;
    }

    public void OnExit(StateController sc) {
        // throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        // immersion dialogue
    }
}
