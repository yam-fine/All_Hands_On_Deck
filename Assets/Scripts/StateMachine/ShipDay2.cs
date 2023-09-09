using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ShipDay2 : StateController {
    public GameObject player;
    public GameObject ship;
    public EventReachedDetection ld;
    public EnviroManager enviro;
    public Transform hull_teleport;
    public float desiredRotation = 25;
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

    public Sail2 sails = new Sail2();
    public Steer2 wheel_steering = new Steer2();
    public Ladder2 ladder_climb = new Ladder2();
    public Final final = new Final();

    // Start is called before the first frame update
    void Start() {
        enviro.configuration = configs[0];
        enviro.Weather.ChangeWeather("Cloudy 1");
        waterMat.SetFloat("_WaveScale", 2);
        waterMat.SetFloat("_WaveFrequency", 1);
        ChangeState(ladder_climb);
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
