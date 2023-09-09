using UnityEngine;

public class GroundCheckLadder : MonoBehaviour {
    public bool IsGrounded(string groundTag, float raycastDistance, LayerMask layer) {
        // Cast a ray down from the character's position.
        RaycastHit hit;
        Vector3 rayStart = transform.position;
        rayStart.y += 0.1f; // Adjust the starting position slightly above the character's feet.

        // Perform the raycast.
        if (Physics.Raycast(rayStart, Vector3.down, out hit, raycastDistance, layer)) {
            // Check if the object hit has the specified ground tag.
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag(groundTag)) {
                return true;
            }
            else {
                return false;
            }

            // Display the raycast by drawing a line in the Scene view.
            Debug.DrawRay(rayStart, Vector3.down * raycastDistance, Color.green);
        }
        else {
            return false;

            // Display the raycast by drawing a line in the Scene view.
            Debug.DrawRay(rayStart, Vector3.down * raycastDistance, Color.red);
        }
    }
}
