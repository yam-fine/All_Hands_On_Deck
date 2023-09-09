using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ShipDay2StateController : StateController {

    public GameObject player;

    public GameObject avatar;
    
    public GameObject ship;

    public GameObject captain;

    public EventReachedDetection ld;
    
    public EnviroManager enviro;
    
    public Transform hull_teleport;
    
    public float desiredRotation = 2;
    //public EventReachedDetection whale_desired_position;
    //public WhaleEventReachedDetection whaleShipColided;
    
    public ActionBasedContinuousMoveProvider actionMoveProvider;
    
    public FadeScreen fade;
    
    public GameObject whale;
    
    public Material waterMat;
    
    public XRSocketInteractor hook_socket;
    
    public Climb player_climb;
    
    public GameObject hook_walls;
    
    public EventReachedDetection hook_pile;
    
    public GameObject rope, hook;
    
    public Canvas hookExplanation;
    
    public List<GameObject> ropeComps;
    
    public List<EnviroConfiguration> configs;
    
    public Transform teleportAfterLadder;
    
    public GroundCheckLadder groundCheck;
    
    public LayerMask ladderGroundLayer;

    public NeedsHelpWillStateControl sailsNPC;


    public AudioManager audioManager;

    public Sail2 sails = new Sail2();
    
    public Steer2 wheel_steering = new Steer2();
    
    public Ladder2 ladder_climb = new Ladder2();

    public WhaleDayTwo whaleState = new WhaleDayTwo();

    public CutsceneDayTwo cutscene = new CutsceneDayTwo();

    public Final final = new Final();


    [HideInInspector] public Dialogue sailsDownDialogue;

    [HideInInspector] public Dialogue sailsUpDialogue;

    [HideInInspector] public Dialogue wheelSteerDialogue;

    [HideInInspector] public Dialogue climbLadderDialogue;

    [HideInInspector] public Dialogue climbLadderEndDialogue;

    [HideInInspector] public Dialogue beforeHullDialogue;

    [HideInInspector] public Dialogue hullDialogue;

    public MusicControlDeckDay1 musicControl;

    public AmbienceControlDeckDay1 ambienceControl;

    public RopePullingInteractor ropePullingInteractor;

    // Start is called before the first frame update
    void Start() {
        enviro.configuration = configs[0];
        enviro.Weather.ChangeWeather("Cloudy 1");
        waterMat.SetFloat("_WaveScale", 2);
        waterMat.SetFloat("_WaveFrequency", 1);

        GameObject dialogueObject = new GameObject("DialogueObject");
        
        sailsUpDialogue = dialogueObject.AddComponent<Dialogue>();
        sailsUpDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.lieut_can_ye_give, GameObject.Find("Will (7)")),
            new DialogueEvents(AudioManager.Sounds.oy_maggots, captain),
            new DialogueEvents(AudioManager.Sounds.i_have_to_do_it, avatar),
        };
        
        sailsDownDialogue = dialogueObject.AddComponent<Dialogue>();
        sailsDownDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.very_well, captain),
        };


        wheelSteerDialogue = dialogueObject.AddComponent<Dialogue>();
        wheelSteerDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.barely_now_to_steer, avatar),
        };

        

        ChangeState(sails);
    }

    public void TeleportWithFade(System.Action<IsState> funcToExecute, IsState state) {
        StartCoroutine(TeleportAndFadeCoroutine(funcToExecute, state));
    }

    private IEnumerator TeleportAndFadeCoroutine(System.Action<IsState> funcToExecute, IsState state) {
        // Fade in
        // fade.FadeIn();
        yield return new WaitForSeconds(fade.fadeDur);

        // Teleport the player
        player.transform.position = hull_teleport.position;
        if (funcToExecute != null)
            funcToExecute.Invoke(state);

        // Fade out
        // fade.FadeOut();
    }

    public void LoadScene() {
        StartCoroutine(LoadSceneIEnum());
    }

    IEnumerator LoadSceneIEnum() {
        yield return new WaitForSeconds(this.fade.fadeDur);
        SceneManager.UnloadSceneAsync("DeckDay1");
        SceneManager.LoadScene("RoomDay2");
    }
}
