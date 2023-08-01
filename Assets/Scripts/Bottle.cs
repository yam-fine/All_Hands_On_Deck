using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bottle : MonoBehaviour
{
    private bool _open;
    [SerializeField] private GameObject cap;

    [SerializeField] private Rigidbody _capRigidBody;

    void Start()
    {
        _capRigidBody.isKinematic = true;
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
        //capInteractable.enabled = true;
    }

    public void OnBottleRelease(SelectExitEventArgs args)
    {
        if (IsOpen())
            return;
        //capInteractable.enabled = false;
    }

    public void OnBottleOpen(SelectExitEventArgs args)
    {
        Debug.Log("Open");
        cap.transform.SetParent(null, worldPositionStays: true);
        _capRigidBody.isKinematic = false;
        Open();
    }
    public void OnBottleClose(SelectEnterEventArgs args)
    {
        Debug.Log("Close");
        Close();
        cap.GetComponent<Rigidbody>().isKinematic = true;
        cap.transform.SetParent(transform, worldPositionStays: true);
    }
}