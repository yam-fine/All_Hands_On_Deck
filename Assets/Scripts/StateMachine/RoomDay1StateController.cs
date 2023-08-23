using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RoomDay1StateController : StateController
{
    public WaterPickup waterPickup = new WaterPickup();
    public Room1Roam room1Roam = new Room1Roam();
    
    [HideInInspector] public ActionBasedContinuousMoveProvider actionMoveProvider;
    [HideInInspector] public ActionBasedContinuousTurnProvider actionTurnProvider;
    [HideInInspector] public Animator avatarAnimator;
    
    [HideInInspector] public Dialogue roamDialogue;

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
    public override void Start() {
        base.Start();
        actionMoveProvider = XrOrigin.GetComponent<ActionBasedContinuousMoveProvider>();
        actionTurnProvider = XrOrigin.GetComponent<ActionBasedContinuousTurnProvider>();
        avatarAnimator = GameManager.Instance.Player.transform.Find("Avatar").GetComponent<Animator>();
        GameObject dialogueObject = new GameObject("DialogueObject");
        roamDialogue = dialogueObject.AddComponent<Dialogue>();
        roamDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.ay_cap, XrOrigin),
            new DialogueEvents(AudioManager.Sounds.avast, captain),
            new DialogueEvents(AudioManager.Sounds.pfft, XrOrigin)
        };

        ChangeState(waterPickup);
    }
}
