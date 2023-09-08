using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AmbienceControlDeckDay1 : MonoBehaviour
{

    public FMODUnity.EventReference fmodEvent;

    public RopePullingInteractor rope;

    private FMOD.Studio.EventInstance ambienceEmitter;

    private bool isStorm = false;
    
    void Start() {
        ambienceEmitter = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        ambienceEmitter.start();
    }

    void Update() {

        if (isStorm) {
            float storm;
            ambienceEmitter.getParameterByName("Storm", out storm);
            ambienceEmitter.setParameterByName("Storm",  storm + Time.deltaTime * 0.04f);
        }

        ambienceEmitter.setParameterByName("SAIL_UP",  1f - rope.GetSailStatus());

    }

    public void Stop() {
        ambienceEmitter.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    public void StartStorm() {
        isStorm = true; 
    }


    public void SetHallParam(bool isHall) {
        if (isHall) {ambienceEmitter.setParameterByName("Hall",  1f);} else {ambienceEmitter.setParameterByName("Hall",  0f);}
    }
    

    
    
}
