using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public float fadeDur = 4;

    [SerializeField] Color fadeColor;

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void FadeIn() {
        Fade(1, 0);
    }

    public void FadeOut() {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut) {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut) {
        float timer = 0;
        while(timer <= fadeDur) {
            Color newCol = fadeColor;
            newCol.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDur);
            rend.material.SetColor("_Color", newCol);
            timer += Time.deltaTime;
            yield return null;
        }
        Color newCol2 = fadeColor;
        newCol2.a = alphaOut;
        rend.material.SetColor("_Color", newCol2);
    }
}
