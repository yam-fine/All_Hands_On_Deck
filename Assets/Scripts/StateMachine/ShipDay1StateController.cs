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

    public GameObject ship;
    public EventReachedDetection ld;
    public EnviroManager enviro;
    public Transform hull_teleport;
    public float desiredRotation = 25;
    public EventReachedDetection whale_desired_position;
    public WhaleEventReachedDetection whaleShipColided;
    public ActionBasedContinuousMoveProvider actionMoveProvider;
    public FadeScreen fade;
    public GameObject whale;
    public Material waterMat;
    public List<EnviroConfiguration> configs;

    public FollowThePath killerWhalePath;
    public PirateManStateControl givingCPR;
    public TalkingWillStateControl takingCPR;

    public NeedsHelpWillStateControl sailsNPC;

    public SailFurling sails = new SailFurling();
    public WheelSteering wheel_steering = new WheelSteering();
    public LadderClimbing ladder_climb = new LadderClimbing();
    public HullDayOne hull = new HullDayOne();
    public WhaleDayOne whaleState = new WhaleDayOne();
    public FinishDayOne finish = new FinishDayOne();

    public AudioManager audioManager;

    public RopePullingInteractor ropePullingInteractor;

    [HideInInspector] public Dialogue sailsDownDialogue;

    [HideInInspector] public Dialogue sailsUpDialogue;

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

        

        enviro.configuration = configs[0];
        enviro.Weather.ChangeWeather("Cloudy 1");
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
        Destroy(GameObject.Find("AvatarPlayer"));//.SetActive(false);
        SceneManager.UnloadSceneAsync("DeckDay1");
        SceneManager.LoadScene("RoomDay2");
    }
}
