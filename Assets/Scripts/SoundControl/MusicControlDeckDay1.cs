using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MusicControlDeckDay1 : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter fmodEmitter;

    private FMOD.Studio.EventInstance musicEmitter;

    public Transform avatar;

    public Transform target;

    public bool hall;

    
    void Start() {
        musicEmitter = fmodEmitter.EventInstance;
    }

    void Update() {
        musicEmitter.setParameterByName("UP_TheLadder",  (float)(1 - (Mathf.Abs(avatar.position.y - target.position.y) / 20.5f) )); 

        if (hall) {
            float intensity;
            musicEmitter.getParameterByName("Hall_Intensity", out intensity);
            musicEmitter.setParameterByName("Hall_Intensity",  intensity + Time.deltaTime * 0.03f);
        }
    }

    public void SetHallParam(bool isHall) {
        if (isHall) {musicEmitter.setParameterByName("Hall",  1f); hall = true; } else {musicEmitter.setParameterByName("Hall",  0f); }
    }

    public void Stop() {
        musicEmitter.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    

    
    
}
