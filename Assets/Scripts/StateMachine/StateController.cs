using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    IsState currentState;

    public WaterPickup waterPickup = new WaterPickup();
    public Room1Roam room1Roam = new Room1Roam();

    [HideInInspector] public Dialogue roamDialogue;

    public GameObject player;
    public GameObject captain;

    private void Start() {
        GameObject dialogueObject = new GameObject("DialogueObject");
        roamDialogue = dialogueObject.AddComponent<Dialogue>();
        roamDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.ay_cap, player),
            new DialogueEvents(AudioManager.Sounds.avast, captain),
            new DialogueEvents(AudioManager.Sounds.pfft, player)
        };

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
