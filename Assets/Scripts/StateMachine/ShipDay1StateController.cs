using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ShipDay1StateController : StateController
{
    public GameObject player;
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

    public FollowThePath killerWhalePath;
    public PirateManStateControl givingCPR;
    public TalkingWillStateControl takingCPR;

    public SailFurling sails = new SailFurling();
    public WheelSteering wheel_steering = new WheelSteering();
    public LadderClimbing ladder_climb = new LadderClimbing();
    public HullDayOne hull = new HullDayOne();
    public WhaleDayOne whaleState = new WhaleDayOne();
    public FinishDayOne finish = new FinishDayOne();

    // Start is called before the first frame update
    void Start()
    {
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
