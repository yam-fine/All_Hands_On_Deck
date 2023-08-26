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

    //public FollowThePath killerWhalePath;
    //public PirateManStateControl givingCPR;
    //public TalkingWillStateControl takingCPR;

    public Sail2 sails = new Sail2();
    public Steer2 wheel_steering = new Steer2();
    public Ladder2 ladder_climb = new Ladder2();

    // Start is called before the first frame update
    void Start() {
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
