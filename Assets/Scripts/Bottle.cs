using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bottle : MonoBehaviour
{
    private bool _open;
    [SerializeField] private XRGrabInteractable cap;

    void Start()
    {
<<<<<<< Updated upstream
        cap.enabled = false;
=======
        capInteractable = cap.GetComponent<XRGrabInteractable>();
        capInteractable.enabled = true;
        cap.GetComponent<Rigidbody>().isKinematic = true;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        cap.enabled = true;
=======
        //capInteractable.enabled = true;
>>>>>>> Stashed changes
    }

    public void OnBottleRelease(SelectExitEventArgs args)
    {
        if (IsOpen())
            return;
<<<<<<< Updated upstream
        cap.enabled = false;
=======
        //capInteractable.enabled = false;
>>>>>>> Stashed changes
    }

    public void OnBottleOpen(SelectExitEventArgs args)
    {
        Debug.Log("Open");

        Open();
<<<<<<< Updated upstream
    }
    public void OnBottleClose(SelectEnterEventArgs args)
    {
        //Debug.Log("Close");
        //Close();
=======
        cap.GetComponent<Rigidbody>().isKinematic = false;
        //cap.transform.parent = null;
    }
    public void OnBottleClose(SelectEnterEventArgs args)
    {
        Debug.Log("Close");
        Close();
        cap.GetComponent<Rigidbody>().isKinematic = true;
        //cap.transform.parent = transform;
>>>>>>> Stashed changes
    }
}