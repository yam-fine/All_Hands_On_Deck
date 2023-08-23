using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enviro;

public class ShipDay1StateController : StateController
{
    public GameObject player;
    public GameObject ship;
    public LadderDetection ld;
    public EnviroManager enviro;
    public Transform hull_teleport;
    public float desiredRotation = 25;

    public SailFurling sails = new SailFurling();
    public WheelSteering wheel_steering = new WheelSteering();
    public LadderClimbing ladder_climb = new LadderClimbing();
    public HullDayOne hull = new HullDayOne();
    public WhaleDayOne whale = new WhaleDayOne();

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(sails);
    }
}
