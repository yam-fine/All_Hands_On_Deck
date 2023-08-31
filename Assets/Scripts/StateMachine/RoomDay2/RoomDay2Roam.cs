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
        sc.player.GetComponent<CharacterController>().enabled = false;
    }

    public void OnExit(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController sc) {
        throw new System.NotImplementedException();
    }
}
