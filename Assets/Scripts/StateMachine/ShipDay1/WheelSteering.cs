using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSteering : IsState
{
    float rotationThreshold = 1;

    private int nextUpdate;

    public void OnEnter(StateController sc) {
        nextUpdate = (int)(Time.time + 15f);
        InnerEnterState((ShipDay1StateController)sc);
        
    }

    private void InnerEnterState(ShipDay1StateController sc) {
        

    }

    public void OnExit(StateController sc) {
        
    }

    public void UpdateState(StateController sc) {
        InnerUpdateState((ShipDay1StateController)sc);
    }

    void InnerUpdateState(ShipDay1StateController sc) {

        // Dialogue
        if(Time.time >= nextUpdate){
    		nextUpdate = Mathf.FloorToInt(Time.time) + 15; // play every 30 seconds
    		sc.wheelSteerDialogue.PlayDialogue(sc, 15, true);
    	}
        

        // Debug.Log("ship rot: " + sc.ship.transform.localRotation.eulerAngles.y + " || " + "desired rot: " + sc.desiredRotation);
        if (Mathf.Abs(sc.ship.transform.localRotation.eulerAngles.y - sc.desiredRotation) <= rotationThreshold) {
            sc.ChangeState(sc.ladder_climb);
        }
    }
}
