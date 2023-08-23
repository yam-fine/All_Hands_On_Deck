using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class GameManager : Singleton<GameManager>
{
    public GameObject Player;
    public XROrigin PlayerXrOrigin;
    public Transform MainMenuStartPoistion;
    public Transform RoomSpawnPoint;
    public Transform DeckSpawnPoint;
    public XRDirectInteractor LeftHand;
    public XRDirectInteractor RightHand;

    [SerializeField] private AnimatorController DeckAnimatorController;
    [SerializeField] private AnimatorController RoomAnimatorController;

    [SerializeField] private InputActionAsset _actionAsset;

    private bool isUpPressed;
    private bool isActivatePressed;
    private InputAction _thumbstick;


    public enum Scenes
    {
        MAIN,
        MainMenu,
        RoomDay1,
        DeckDay1
    }
    
    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        var activate = _actionAsset.FindActionMap("XRI RightHand").FindAction("Activate");
        activate.Enable();
        activate.started += OnActivatePress;
        activate.canceled += OnActivateCancle;

        _thumbstick = _actionAsset.FindActionMap("XRI RightHand").FindAction("Move1");
        _thumbstick.Enable();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MAIN")
        {
            LoadScene(Scenes.MainMenu);
        }
        if(scene.name == Scenes.MainMenu.ToString())
        {
            MainMenuInitialize();
        }
        if (scene.name == Scenes.RoomDay1.ToString())
        {
            RoomDayOneInitialize();
        }
        if (scene.name == Scenes.DeckDay1.ToString())
        {
            DeckDayOneInitialize();
        }
        Invoke("StupidThing", 2);
    }

    private void MainMenuInitialize()
    {
        // Play sound
        Player.transform.position = MainMenuStartPoistion.position;
        PlayerXrOrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        PlayerXrOrigin.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;
    }

    private void DeckDayOneInitialize()
    {
        // Play Sound
        Player.transform.position = DeckSpawnPoint.transform.position;
        Player.transform.rotation = DeckSpawnPoint.transform.rotation;
        PlayerXrOrigin.GetComponent<CharacterController>().enabled = false;
        Player.transform.Find("Avatar").GetComponent<Animator>().runtimeAnimatorController = DeckAnimatorController;

    }

    private void RoomDayOneInitialize()
    {
        // Play Sound
        Player.transform.position = RoomSpawnPoint.transform.position;
        Player.transform.rotation = RoomSpawnPoint.transform.rotation;
        PlayerXrOrigin.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        PlayerXrOrigin.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;
        PlayerXrOrigin.GetComponent<CharacterController>().enabled = false;
        Player.transform.Find("Avatar").GetComponent<Animator>().runtimeAnimatorController = RoomAnimatorController;
    }

    private void OnActivatePress(InputAction.CallbackContext context)
    {
        isActivatePressed = true;
    }

    private void OnActivateCancle(InputAction.CallbackContext context)
    {
        isActivatePressed = false;
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)scene);
    }


    public void StupidThing()
    {
        // You might be asking yourself WHYYYY??! and my answer is -> because it fixes a stupid bug i couldnt fix in any other awy.
        // Every time you load scene we loose all references to the hands, this fixes it.
        
        RightHand.gameObject.SetActive(false);
        RightHand.gameObject.SetActive(true);
        LeftHand.gameObject.SetActive(false);
        LeftHand.gameObject.SetActive(true);

    }
    
    IEnumerator WaitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
        }
    }

    private void Update()
    {
        if (_thumbstick.ReadValue<Vector2>().y > 0.5f)
        {

            isUpPressed = true;
        }
        else isUpPressed = false;

        if(isActivatePressed && isUpPressed) // i mean the up on the right controller.
        {
            Restart();
        }
    }

    public void Restart()
    {
        
        LoadScene(Scenes.MainMenu);
    }

   
}