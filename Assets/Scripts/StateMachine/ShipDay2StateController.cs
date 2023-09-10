using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ShipDay2StateController : StateController {

    public GameObject player;

    public GameObject xrorigin;

    public GameObject avatar;

    public WhaleEventReachedDetection whaleShipColided;
    
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

    public FollowThePath killerWhalePath;
    
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

    public Final finish = new Final();


    [HideInInspector] public Dialogue sailsDownDialogue;

    [HideInInspector] public Dialogue sailsUpDialogue;

    [HideInInspector] public Dialogue wheelSteerDialogue;

    [HideInInspector] public Dialogue climbLadderDialogue;

    [HideInInspector] public Dialogue climbLadderEndDialogue;

    [HideInInspector] public Dialogue whaleDialogue;

    [HideInInspector] public Dialogue finishDialogue;

    public MusicControlDeckDay1 musicControl;

    public AmbienceControlDeckDay1 ambienceControl;

    public RopePullingInteractor ropePullingInteractor;

    public Animator capAnimator;

    public EventReachedDetection cutscene_desired_position;

    public FollowThePath captainCutScenePath;

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

        climbLadderDialogue = dialogueObject.AddComponent<Dialogue>();
        climbLadderDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.lieut_take_this, captain),
            new DialogueEvents(AudioManager.Sounds.cap_im_not_sure, avatar),
            new DialogueEvents(AudioManager.Sounds.oh_but_i_have_never, captain),
            new DialogueEvents(AudioManager.Sounds.i_dont_know_cap, avatar),
            new DialogueEvents(AudioManager.Sounds.shut_ye_trap, captain),
            new DialogueEvents(AudioManager.Sounds.im_not_so_sure, avatar),
        };

        climbLadderEndDialogue = dialogueObject.AddComponent<Dialogue>();
        climbLadderEndDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.we_are_on_tail_cap, xrorigin),
            new DialogueEvents(AudioManager.Sounds.hunting_station, captain),
        };

        whaleDialogue = dialogueObject.AddComponent<Dialogue>();
        whaleDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.come_to_papa, captain),
        };


        finishDialogue = dialogueObject.AddComponent<Dialogue>();
        finishDialogue.dialogueEvents = new List<DialogueEvents>{
            new DialogueEvents(AudioManager.Sounds.i_just_had, avatar),
        };
        

        // ChangeState(sails);
        ChangeState(sails);
    }

    public void TeleportWithFade(System.Action<IsState> funcToExecute, IsState state) {
        StartCoroutine(TeleportAndFadeCoroutine(funcToExecute, state));
    }

    public void FadeFinish() {
        StartCoroutine(FadeFinishCoroutine());
    }

    private IEnumerator TeleportAndFadeCoroutine(System.Action<IsState> funcToExecute, IsState state) {
        
        yield return new WaitForSeconds(5);

        // Fade out
        fade.FadeOut();
        yield return new WaitForSeconds(fade.fadeDur);

        // Teleport the player
        xrorigin.transform.position = hull_teleport.position;

        if (funcToExecute != null)
            funcToExecute.Invoke(state);

        // Fade in
        fade.FadeIn();

        actionMoveProvider.enabled = true;

    }


    private IEnumerator FadeFinishCoroutine() {

        // Fade out
        fade.FadeOut();
        yield return new WaitForSeconds(fade.fadeDur);

        finishDialogue.PlayDialogue(this);

        yield return new WaitForSeconds(5);

        Application.Quit();

    }

}
