using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AmbienceControlDeckDay1 : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter fmodEmitter;

    public RopePullingInteractor rope;

    private FMOD.Studio.EventInstance ambienceEmitter;

    private bool isStorm = false;
    
    void Start() {
        // I used fmod sound emitter and not AudioManager because of a bug in AudioManager - when playing a sound, it plays it from the current location of the avatar. If the sound is long enough (for instance - rain), the player will hear it getting weaker as they move in the scene.
        // I used fmod event emitter and not EventInstance due to a bug, not hearing some sounds when headset is off. IDK why it happens.
        ambienceEmitter = fmodEmitter.EventInstance;
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

    public void StartRainAtLadder() {
        ambienceEmitter.setParameterByName("Storm", 0.3f);
    }
    

    
    
}
