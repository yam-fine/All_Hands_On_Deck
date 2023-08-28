using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRDirectInteractor))]
public class BlockCupHolding : MonoBehaviour
{
    [SerializeField] private InteractionLayerMask _cupLayer;
    private XRDirectInteractor hand;

    public void Start()
    {
        hand = GetComponent<XRDirectInteractor>();
        hand.interactionLayers &= ~_cupLayer;
    }

    public void OnBottleHold(SelectEnterEventArgs args)
    {
        AllowCapRemoval();
    }

    public void OnBottleRelease(SelectExitEventArgs args)
    {
        ForbidCapRemoval();
    }

    public void AllowCapRemoval()
    {
        hand.interactionLayers = ~0;
    }

    public void ForbidCapRemoval()
    {
        hand.interactionLayers &= ~_cupLayer;
    }


}
