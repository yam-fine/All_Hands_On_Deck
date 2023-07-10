using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Vector3 startPoint;    // The starting point of the enemy
    public Vector3 endPoint;      // The destination point of the enemy
    public float movementSpeed;     // The speed at which the enemy moves towards the destination
    
    private void Start() {
        transform.position = startPoint;
    }

    private void Update() {
        // Move the enemy towards the destination based on the movement speed
        transform.position = Vector3.MoveTowards(transform.position, endPoint, movementSpeed * Time.deltaTime);
        
        // Check if the enemy has reached the destination
        if (transform.position == endPoint) {
            Reached();
        }
    }

    void Reached() {
        bool dontSwitch = Random.value < 0.5f;
        SharkSpawner.Instance.Respawn(gameObject,
            dontSwitch ? startPoint : endPoint,
            dontSwitch ? endPoint : startPoint);
    }
}