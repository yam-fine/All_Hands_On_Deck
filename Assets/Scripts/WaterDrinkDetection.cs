using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrinkDetection : MonoBehaviour
{
    private void OnParticleCollision(GameObject other) {
        Debug.Log("water drop");
    }
}
