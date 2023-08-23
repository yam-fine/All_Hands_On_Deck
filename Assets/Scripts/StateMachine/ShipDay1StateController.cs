using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enviro;

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

    public GameObject killerWhale;

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

    public void TeleportWithFade() {
        StartCoroutine(TeleportAndFadeCoroutine());
    }

    private IEnumerator TeleportAndFadeCoroutine() {
        // Fade out
        yield return StartCoroutine(FadeOut());

        // Teleport the player
        player.transform.position = hull_teleport.position;

        // Fade in
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut() {
        float elapsedTime = 0;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1); // Black color

        while (elapsedTime < fadeDuration) {
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;
    }

    private IEnumerator FadeIn() {
        float elapsedTime = 0;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 0); // Transparent color

        while (elapsedTime < fadeDuration) {
            fadeImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;
    }
}
