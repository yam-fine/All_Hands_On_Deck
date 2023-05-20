using UnityEngine;

public class Buoyancy : MonoBehaviour {
    [SerializeField] float swayAmount = 1f; // Parameter to control the amount of sway
    [SerializeField] float swayFrequency = 1f; // Parameter to control the frequency of the sway

    private float originalY;
    private float timeCounter;

    private void Start() {
        originalY = transform.position.y;
        timeCounter = 0f;
    }

    private void Update() {
        // Calculate the sway angle using a sine wave based on time
        float swayAngle = Mathf.Sin(timeCounter * swayFrequency) * swayAmount;

        // Rotate the ship around the up vector (y-axis) to simulate sway
        transform.rotation = Quaternion.Euler(0f, 0f, swayAngle);

        // Increment the time counter based on time to control the speed of sway
        timeCounter += Time.deltaTime;
    }
}
