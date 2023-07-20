using UnityEngine;

public class LanternLight : MonoBehaviour {
    public float minIntensity = 0.5f;        // Minimum intensity of the Point Light
    public float maxIntensity = 2.0f;        // Maximum intensity of the Point Light
    public float flickerSpeed = 1.0f;        // Speed of flickering

    private Light pointLight;
    private float targetIntensity;

    private void Start() {
        pointLight = GetComponent<Light>();
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        pointLight.intensity = targetIntensity;
    }

    private void Update() {
        // Calculate the new intensity using lerp
        float newIntensity = Mathf.Lerp(pointLight.intensity, targetIntensity, Time.deltaTime * flickerSpeed);

        // Apply the new intensity to the Point Light
        pointLight.intensity = newIntensity;

        // Check if the intensity has reached the target, if so, set a new target
        if (Mathf.Abs(pointLight.intensity - targetIntensity) <= 0.01f) {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
