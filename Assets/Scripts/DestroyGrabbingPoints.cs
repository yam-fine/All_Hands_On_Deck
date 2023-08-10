using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyGrabbingPoints : MonoBehaviour
{
    [SerializeField] private RopePullingInteractor ropePullingPrefab;
    [SerializeField] private GrabbingPointSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabbingPoint"))
        {
            GrabbingPoint grabingObject = other.GetComponent<GrabbingPoint>();
            if (!grabingObject.IsAnchor)
            {
                ropePullingPrefab.RemoveGrabbingPoint(other.GetComponent<XRGrabInteractable>());
                Destroy(grabingObject.gameObject);
                spawner.createdGrabbingPoints--;
                return;
            }

        }
    }
}
