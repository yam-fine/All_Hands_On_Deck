using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HallParamTrigger : MonoBehaviour
{

    public MusicControlDeckDay1 musicControl;
    public AmbienceControlDeckDay1 ambienceControl;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "XR Origin") {
            ambienceControl.SetHallParam(true);
            musicControl.SetHallParam(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "XR Origin") {
            ambienceControl.SetHallParam(false);
            musicControl.SetHallParam(false);
        }
    }
    

    
    
}


