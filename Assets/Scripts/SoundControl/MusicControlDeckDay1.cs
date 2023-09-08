using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MusicControlDeckDay1 : MonoBehaviour
{

    public FMODUnity.EventReference fmodEvent;

    private FMOD.Studio.EventInstance musicEmitter;

    public Transform player;

    public Transform target;

    private bool isStorm;

    void Start() {
        musicEmitter = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        musicEmitter.start();
        musicEmitter.setParameterByName("Deck",  1f); 
    }

    void Update() {
        musicEmitter.setParameterByName("UP_TheLadder",  (float)(1 - (Mathf.Abs(player.position.y - target.position.y) / 20.5f) )); 
    }

    public void SetHallParam(bool isHall) {
        if (isHall) {musicEmitter.setParameterByName("Hall",  1f); } else {musicEmitter.setParameterByName("Hall",  0f); }  
        
    }

    

    
    
}
