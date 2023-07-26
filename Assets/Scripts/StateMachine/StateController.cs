using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    IsState currentState;

    public WaterPickup waterPickup = new WaterPickup();
    public Room1Roam room1Roam = new Room1Roam();

    public GameObject player;
    public GameObject captain;

    private void Start() {
        ChangeState(waterPickup);
    }

    private void Update() {
        if (currentState != null)
            currentState.UpdateState(this);
    }

    public void ChangeState(IsState newState) {
        if (currentState != null)
            currentState.OnExit(this);
        currentState = newState;
        currentState.OnEnter(this);
    }
}

public interface IsState {
    public void OnEnter(StateController sc);
    public void UpdateState(StateController sc);
    public void OnExit(StateController sc);
}
