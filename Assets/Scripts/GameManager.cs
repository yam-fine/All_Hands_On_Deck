using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform MainMenuStartPoistion;
    [SerializeField] private Transform roomSpawnPoint;
    [SerializeField] private Transform deckSpawnPoint;
    [SerializeField] private XRDirectInteractor LeftHand;
    [SerializeField] private XRDirectInteractor RightHand;

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
        SceneManager.sceneLoaded += OnMainSceneLoaded;
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

    void OnMainSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MAIN")
        {
            LoadScene(Scenes.MainMenu);
        }
        Invoke("StupidThing", 2);
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

    public void StartGame()
    {
        
    }

    public void StupidThing()
    {
        // You might be asking yourself WHYYYY??! and my answer is -> because it fixes a stupid bug i couldnt fix in any other awy.
        // Every time you load scene we loose all references to the hands, this fixes it.
        
        RightHand.enabled = false;
        RightHand.enabled = true;
        LeftHand.enabled = false;
        LeftHand.enabled = true;
        
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