using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bottle : MonoBehaviour
{
    private bool _open;
    private XRGrabInteractable _bottleInteratable;
    [SerializeField] private GameObject cap;

    [SerializeField] private Rigidbody _capRigidBody;

    [SerializeField] private Transform _rightGrab;
    [SerializeField] private Transform _leftGrab;

    public event Action PlayerDrank;

    private AudioManager audioManager;


    void Awake() {
        audioManager = GameObject.Find("Avatar").GetComponent<AudioManager>();
    }

    void Start()
    {
        _capRigidBody.isKinematic = true;
        _bottleInteratable = GetComponent<XRGrabInteractable>();   
    }

    void Update()
    {
        
    }

    public bool IsOpen()
    {
        return _open;
    }

    public void Open()
    {
        _open = true;
    }

    public void Close()
    {
        _open = false;
    }

    public void OnBottleHold(SelectEnterEventArgs args)
    {

        if (audioManager) {audioManager.PlaySound(AudioManager.Sounds.bottle_up);}
        

        if (args.interactor.name.Contains("Left"))
        {
            _bottleInteratable.attachTransform = _leftGrab;
        }
        else 
            _bottleInteratable.attachTransform = _rightGrab;
        if (IsOpen())
            return;
    }

    public void OnBottleRelease(SelectExitEventArgs args)
    {
        if (audioManager) {audioManager.PlaySound(AudioManager.Sounds.bottle_down);}

        if (IsOpen())
            return;
    }

    public void OnBottleOpen(SelectExitEventArgs args)
    {
        // Debug.Log("Open");
        
        if (audioManager) {audioManager.PlaySound(AudioManager.Sounds.bottle_open);}

        cap.transform.SetParent(null, worldPositionStays: true);
        _capRigidBody.isKinematic = false;
        Open();
    }
    public void OnBottleClose(SelectEnterEventArgs args)
    {
        if (audioManager) {audioManager.PlaySound(AudioManager.Sounds.bottle_close);}
        Close();
        cap.GetComponent<Rigidbody>().isKinematic = true;
        cap.transform.SetParent(transform, worldPositionStays: true);
    }

    public void Drink()
    {
        PlayerDrank?.Invoke();
    }
}