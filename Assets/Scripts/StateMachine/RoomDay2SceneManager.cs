using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Enviro;

public class RoomDay2SceneManager : StateController
{
    public WaterPickup2 waterPickup = new WaterPickup2();
    public RoomDay2Roam roam = new RoomDay2Roam();

    public ActionBasedContinuousMoveProvider actionMoveProvider;
    public ActionBasedContinuousTurnProvider actionTurnProvider;
    public Material waterMat;
    public GameObject player;
    public GameObject captain;
    public Bottle bottle;
    public Animator avatarAnimator;
    public EnviroConfiguration config;
    public EnviroManager enviro;

    private bool _playerDrank;
    public bool PlayerDrank { get { return _playerDrank; } }

    // Start is called before the first frame update
    void Start()
    {
        enviro.configuration = config;
        enviro.Weather.ChangeWeather("Cloudy 1");
        waterMat.SetFloat("_WaveScale", 2);
        waterMat.SetFloat("_WaveFrequency", 1);

        ChangeState(waterPickup);
    }

    public void OnEnable() {
        bottle.PlayerDrank += OnPlayerDrank;
    }

    public void OnDisable() {
        bottle.PlayerDrank -= OnPlayerDrank;
    }

    public void OnPlayerDrank() {
        _playerDrank = true;
        bottle.PlayerDrank -= OnPlayerDrank; // no need to detect anymore
    }
}
