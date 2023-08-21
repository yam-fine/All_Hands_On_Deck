using UnityEngine;

public class Buoyancy : MonoBehaviour {
    [SerializeField] float swayAmount = 1f; // Parameter to control the amount of sway
    [SerializeField] float swayFrequency = 1f; // Parameter to control the frequency of the sway
    [SerializeField] Transform wheel;
    [SerializeField] float rotationSpeed = 100;

    private float timeCounter;
    float rotateDir = 0;

    private void Start() {
        timeCounter = 0f;
    }

    private void Update() {    
        if (wheel.rotation.x < 0)
            rotateDir += -1;
        else if (wheel.rotation.x > 0)
            rotateDir += 1;

        // Calculate the sway angle using a sine wave based on time
        float swayAngle = Mathf.Sin(timeCounter * swayFrequency) * swayAmount;

        // Rotate the ship around the up vector (y-axis) to simulate sway
        transform.rotation = Quaternion.Euler(0f, rotateDir, swayAngle);

        // Increment the time counter based on time to control the speed of sway
        timeCounter += Time.deltaTime;

        //transform.Rotate(Vector3.up, 1000 * Time.deltaTime);
    }
}
