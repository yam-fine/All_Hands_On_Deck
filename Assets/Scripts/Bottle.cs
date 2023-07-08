using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bottle : MonoBehaviour
{
    private bool _open;
    [SerializeField] private XRGrabInteractable capInteractable;
    [SerializeField] private GameObject cap;

    void Start()
    {
        capInteractable = cap.GetComponent<XRGrabInteractable>();
        capInteractable.enabled = false;
        cap.GetComponent<Rigidbody>().isKinematic = true;
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
        if (IsOpen())
            return;
        capInteractable.enabled = true;
    }

    public void OnBottleRelease(SelectExitEventArgs args)
    {
        if (IsOpen())
            return;
        capInteractable.enabled = false;
    }

    public void OnBottleOpen(SelectExitEventArgs args)
    {
        Debug.Log("Open");
        Open();
        cap.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void OnBottleClose(SelectEnterEventArgs args)
    {
        Debug.Log("Close");
        Close();
        cap.GetComponent<Rigidbody>().isKinematic = true;
    }
}