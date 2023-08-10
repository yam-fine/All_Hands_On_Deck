using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbingPointSpawner : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabbingPointPrefab;
    [SerializeField] private RopePullingInteractor ropePullingPrefab;
    private float offset = 0.25f;
    public int createdGrabbingPoints = 0;
   
    public XRGrabInteractable Spawn()
    {
        Debug.Log("SPAWN");
        
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y+offset, transform.position.z);
        createdGrabbingPoints++;
        Debug.Log(newPos);
        XRGrabInteractable newGrabbingPoint = Instantiate(grabbingPointPrefab, newPos, transform.rotation);
        return newGrabbingPoint;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GrabbingPoint"))
        {
            GrabbingPoint grabingObject = other.GetComponent<GrabbingPoint>();
            if (grabingObject.IsUsedForSpawn && !grabingObject.IsAnchor)
            {
                ropePullingPrefab.RemoveGrabbingPoint(other.GetComponent<XRGrabInteractable>());
                Destroy(grabingObject.gameObject);
                createdGrabbingPoints--;
                return;
            }
            XRGrabInteractable grabbingPoint = Spawn();
            ropePullingPrefab.AddGrabingPoint(grabbingPoint, new Vector3(transform.position.x, transform.position.y + (offset*createdGrabbingPoints), transform.position.z));
            grabingObject.IsUsedForSpawn = true;

        }
    }
}
