using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDay2Roam : IsState
{
    public void OnEnter(StateController sc) {
        // dialouge 
        InnerOnEnter((RoomDay2SceneManager)sc);
    }

    void InnerOnEnter(RoomDay2SceneManager sc) {
        sc.xrorigin.GetComponent<CharacterController>().enabled = false;
        sc.roamDialogue.PlayDialogue(sc, 2, true);
    }

    public void OnExit(StateController sc) {

    }

    public void UpdateState(StateController sc) {

    }
}
