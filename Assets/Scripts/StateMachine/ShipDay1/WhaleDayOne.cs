using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WhaleDayOne : IsState
{

    private ShipDay1StateController state;

    public void OnEnter(StateController sc) {
        state = (ShipDay1StateController)sc;
        state.killerWhalePath.Attack();
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
