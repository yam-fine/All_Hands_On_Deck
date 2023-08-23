using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enviro;
using UnityEngine.XR.Interaction.Toolkit;


public class ShipDay1StateController : StateController
{
    public GameObject player;
    public GameObject ship;
    public EventReachedDetection ld;
    public EnviroManager enviro;
    public Transform hull_teleport;
    public float desiredRotation = 25;
    public Image fadeImage;
    public EventReachedDetection whale_desired_position;
    public ActionBasedContinuousMoveProvider actionMoveProvider;
    public FadeScreen fade;

    public FollowThePath killerWhalePath;

    public SailFurling sails = new SailFurling();
    public WheelSteering wheel_steering = new WheelSteering();
    public LadderClimbing ladder_climb = new LadderClimbing();
    public HullDayOne hull = new HullDayOne();
    public WhaleDayOne whale = new WhaleDayOne();

    float fadeDuration = 1;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(sails);
    }

    public void TeleportWithFade(System.Action<IsState> funcToExecute, IsState state) {
        StartCoroutine(TeleportAndFadeCoroutine(funcToExecute, state));
    }

    private IEnumerator TeleportAndFadeCoroutine(System.Action<IsState> funcToExecute, IsState state) {
        // Fade in
        fade.FadeIn();
        yield return new WaitForSeconds(fade.fadeDur);

        // Teleport the player
        player.transform.position = hull_teleport.position;
        if (funcToExecute != null)
            funcToExecute.Invoke(state);

        // Fade out
        fade.FadeOut();
    }
}
