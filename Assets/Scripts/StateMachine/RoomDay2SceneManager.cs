using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Enviro;

public class RoomDay2SceneManager : StateController
{
    public WaterPickup2 waterPickup = new WaterPickup2();
    public RoomDay2Roam roam = new RoomDay2Roam();

    public ActionBasedContinuousMoveProvider actionMoveProvider;
    public ActionBasedContinuousTurnProvider actionTurnProvider;
    public Material waterMat;
    public GameObject player;

    public GameObject xrorigin;

    public GameObject captain;

    public GameObject avatar;

    public Transform getUpPosition;

    public Bottle bottle;
    public Animator avatarAnimator;
    public EnviroConfiguration config;
    public EnviroManager enviro;

    public AudioManager audioManager;

    [HideInInspector] public Dialogue waterDrinkDialogue;

    [HideInInspector] public Dialogue roamDialogue;



    private bool _playerDrank;
    public bool PlayerDrank { get { return _playerDrank; } }

    // Start is called before the first frame update
    void Start()
    {
        enviro.configuration = config;
        enviro.Weather.ChangeWeather("Cloudy 1");
        waterMat.SetFloat("_WaveScale", 2);
        waterMat.SetFloat("_WaveFrequency", 1);


        // init dialogue
        GameObject dialogueObject = new GameObject("DialogueObject");

        waterDrinkDialogue = dialogueObject.AddComponent<Dialogue>();
        waterDrinkDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.ah_its_morning_day2, avatar),
            new DialogueEvents(AudioManager.Sounds.what, avatar),
            new DialogueEvents(AudioManager.Sounds.ah_your_awake, captain),
            new DialogueEvents(AudioManager.Sounds.what_in_the_hell, avatar),
            new DialogueEvents(AudioManager.Sounds.il_leave_ya, avatar),
        };
        
        roamDialogue = dialogueObject.AddComponent<Dialogue>();
        roamDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.get_it_together, avatar),
            new DialogueEvents(AudioManager.Sounds.word_around, GameObject.Find("ImaginaryCrewMate")),
        };


        ChangeState(waterPickup);
    }

    public void OnEnable() {
        bottle.PlayerDrank += OnPlayerDrank;
    }

    public void OnDisable() {
        bottle.PlayerDrank -= OnPlayerDrank;
    }

    public void OnPlayerDrank() {
        _playerDrank = true;
        bottle.PlayerDrank -= OnPlayerDrank; // no need to detect anymore
    }
}
