using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StateController : MonoBehaviour
{
    IsState currentState;

    public WaterPickup waterPickup = new WaterPickup();
    public Room1Roam room1Roam = new Room1Roam();
    
    public ActionBasedContinuousMoveProvider actionMoveProvider;
    public ActionBasedContinuousTurnProvider actionTurnProvider;
    public Animator avatarAnimator;
    

    [HideInInspector] public Dialogue roamDialogue;

    public GameObject player;
    public GameObject captain;
    public Bottle bottle;

    private bool _playerDrank;
    public bool PlayerDrank { get { return _playerDrank; } }

    public void OnEnable()
    {
        bottle.PlayerDrank += OnPlayerDrank;
    }

    public void OnDisable()
    {
        bottle.PlayerDrank -= OnPlayerDrank;
    }

    public void OnPlayerDrank()
    {
        _playerDrank = true;
        bottle.PlayerDrank -= OnPlayerDrank; // no need to detect anymore
    }
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
