using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BottleSocketDay2 : MonoBehaviour
{
    [SerializeField] private BlockCupHolding m_blockCupHolding;
    [SerializeField] private XRSocketInteractor m_socket;

    public void OnBottleEnter()
    {
        m_blockCupHolding.AllowCapRemoval();
    }

}
