using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Enviro;

public class RoomDay1StateController : StateController
{
    public WaterPickup waterPickup = new WaterPickup();
    public Room1Roam room1Roam = new Room1Roam();
    
    public ActionBasedContinuousMoveProvider actionMoveProvider;
    public ActionBasedContinuousTurnProvider actionTurnProvider;
    public Animator avatarAnimator;
    public Material waterMat;

    [HideInInspector] public Dialogue waterDrinkDialogue;

    [HideInInspector] public Dialogue roamDialogue;

    public GameObject player;

    public Transform getUpPosition;
    
    public GameObject captain;
    public Bottle bottle;
    public EnviroConfiguration config;
    public EnviroManager enviro;

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
        enviro.configuration = config;
        enviro.Weather.ChangeWeather("Cloudy 1");
        waterMat.SetFloat("_WaveScale", 2);
        waterMat.SetFloat("_WaveFrequency", 1);

        GameObject dialogueObject = new GameObject("DialogueObject");

        waterDrinkDialogue = dialogueObject.AddComponent<Dialogue>();
        waterDrinkDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.i_just_had, player),
            new DialogueEvents(AudioManager.Sounds.i_should, captain),
        };
        
        roamDialogue = dialogueObject.AddComponent<Dialogue>();
        roamDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.ay_cap, player),
            new DialogueEvents(AudioManager.Sounds.avast, captain),
            new DialogueEvents(AudioManager.Sounds.pfft, player)
        };

        ChangeState(waterPickup);
    }
}
