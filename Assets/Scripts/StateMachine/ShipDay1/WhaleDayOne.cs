using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WhaleDayOne : IsState
{



    public void OnEnter(StateController sc) {
        // dialogue
    }

    public void OnExit(StateController sc) {
        SceneManager.UnloadSceneAsync("DeckDay1");
        SceneManager.LoadScene("RoomDay2");
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {
        
    }
}
