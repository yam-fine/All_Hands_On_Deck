using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ShipDay1StateController : StateController
{
    public GameObject player;

    public GameObject avatar;

    public GameObject captain;

    public GameObject ship;
    public EventReachedDetection ld;
    public EnviroManager enviro;
    public Transform hull_teleport;
    public float desiredRotation = 2;
    public Transform afterLadderPosition;

    public EventReachedDetection hull_desired_position;

    public EventReachedDetection whale_desired_position;

    public EventReachedDetection cutscene_desired_position;

    public WhaleEventReachedDetection whaleShipColided;

    

    public ActionBasedContinuousMoveProvider actionMoveProvider;
    public FadeScreen fade;
    public GameObject whale;
    public Material waterMat;
    public List<EnviroConfiguration> configs;

    public FollowThePath killerWhalePath;

    public FollowThePath playerCutScenePath;

    public PirateManStateControl givingCPR;
    public TalkingWillStateControl takingCPR;

    public NeedsHelpWillStateControl sailsNPC;

    public SailFurling sails = new SailFurling();
    
    public WheelSteering wheel_steering = new WheelSteering();
    
    public LadderClimbing ladder_climb = new LadderClimbing();
    
    public EnteringHullDayOne beforeHull = new EnteringHullDayOne();

    public HullDayOne hull = new HullDayOne();
    
    public WhaleDayOne whaleState = new WhaleDayOne();

    public CutsceneDayOne cutscene = new CutsceneDayOne();
    
    public FinishDayOne finish = new FinishDayOne();

    public AudioManager audioManager;

    public RopePullingInteractor ropePullingInteractor;

    [HideInInspector] public Dialogue sailsDownDialogue;

    [HideInInspector] public Dialogue sailsUpDialogue;

    [HideInInspector] public Dialogue wheelSteerDialogue;

    [HideInInspector] public Dialogue climbLadderDialogue;

    [HideInInspector] public Dialogue climbLadderEndDialogue;

    [HideInInspector] public Dialogue beforeHullDialogue;

    [HideInInspector] public Dialogue hullDialogue;

    public GameObject[] toDisapearOnHull;

    public MusicControlDeckDay1 musicControl;

    public AmbienceControlDeckDay1 ambienceControl;
    

    void Awake() {
        audioManager = GameObject.Find("Avatar").GetComponent<AudioManager>();
    }


    // Start is called before the first frame update
    void Start()
    {

        GameObject dialogueObject = new GameObject("DialogueObject");

        sailsDownDialogue = dialogueObject.AddComponent<Dialogue>();
        sailsDownDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.ye_two, avatar),
        };

        sailsUpDialogue = dialogueObject.AddComponent<Dialogue>();
        sailsUpDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.much_obliged, avatar),
            new DialogueEvents(AudioManager.Sounds.ye_sea_rats, avatar), 
        };


        wheelSteerDialogue = dialogueObject.AddComponent<Dialogue>();
        wheelSteerDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.but_cap_said, avatar),
        };

        climbLadderDialogue = dialogueObject.AddComponent<Dialogue>();
        climbLadderDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.fine_work, captain),
        };

        climbLadderEndDialogue = dialogueObject.AddComponent<Dialogue>();
        climbLadderEndDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.them_black_clouds, avatar),
        };


        beforeHullDialogue = dialogueObject.AddComponent<Dialogue>();
        beforeHullDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.right_maggots, captain),
            new DialogueEvents(AudioManager.Sounds.ay_sir_right_maggots, avatar),
            new DialogueEvents(AudioManager.Sounds.headcount_the_maggots, captain),
            new DialogueEvents(AudioManager.Sounds.ay_sir_headcount, avatar),
        };


        hullDialogue = dialogueObject.AddComponent<Dialogue>();
        hullDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.benji, avatar),
            new DialogueEvents(AudioManager.Sounds.ay_benji, GameObject.Find("Pirate (3)")),
            new DialogueEvents(AudioManager.Sounds.montague, avatar),
            new DialogueEvents(AudioManager.Sounds.ay_montague, GameObject.Find("John (3)")),
            new DialogueEvents(AudioManager.Sounds.george, avatar),
            new DialogueEvents(AudioManager.Sounds.ay_george, GameObject.Find("Will (4)")),
            new DialogueEvents(AudioManager.Sounds.will_john, avatar),
            new DialogueEvents(AudioManager.Sounds.big_wave, avatar),
            new DialogueEvents(AudioManager.Sounds.that_was_not, GameObject.Find("Will (4)")),
            new DialogueEvents(AudioManager.Sounds.will_john_scream, avatar),
        };

        

        enviro.configuration = configs[0];
        enviro.Weather.ChangeWeather("Cloudy 1");
        ChangeState(sails);
        
    }

    public void TeleportWithFade(System.Action<IsState> funcToExecute, IsState state) {
        StartCoroutine(TeleportAndFadeCoroutine(funcToExecute, state));
    }

    private IEnumerator TeleportAndFadeCoroutine(System.Action<IsState> funcToExecute, IsState state) {
        // Fade in
        yield return new WaitForSeconds(5);
        climbLadderEndDialogue.PlayDialogue(this);
        fade.FadeOut();
        yield return new WaitForSeconds(fade.fadeDur);

        // Teleport the player
        player.transform.position = afterLadderPosition.position;
        if (funcToExecute != null)
            funcToExecute.Invoke(state);

        // Fade out
        fade.FadeIn();
    }

    public void LoadScene() {
        StartCoroutine(LoadSceneIEnum());
    }

    IEnumerator LoadSceneIEnum() {
        yield return new WaitForSeconds(this.fade.fadeDur);
        Destroy(GameObject.Find("AvatarPlayer"));//.SetActive(false);
        SceneManager.UnloadSceneAsync("DeckDay1");
        SceneManager.LoadScene("RoomDay2");
    }
}
