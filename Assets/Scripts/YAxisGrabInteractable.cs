using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class YAxisGrabInteractable : XRGrabInteractable
{
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (isSelected)
        {
            // Get the interactor that is currently grabbing the interactable
            XRBaseInteractor interactor = selectingInteractor;

            // Calculate the interactor's position in the interactable's local space
            Vector3 localInteractorPosition = transform.InverseTransformPoint(interactor.transform.position);

            // Restrict the local interactor position to the y-axis
            localInteractorPosition.x = 0;
            localInteractorPosition.z = 0;

            // Convert the local interactor position back to world space
            Vector3 worldInteractorPosition = transform.TransformPoint(localInteractorPosition);

            // Move the interactable to the interactor's position
            transform.position = worldInteractorPosition;
        }

        base.ProcessInteractable(updatePhase);
    }
}
