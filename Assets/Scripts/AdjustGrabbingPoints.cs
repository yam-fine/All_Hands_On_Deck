using UnityEngine;

[ExecuteInEditMode]
public class AdjustGrabbingPoints : MonoBehaviour
{
    public float spacing = 0.25f;

    void Update()
    {
        if (!Application.isPlaying)
        {
            AdjustPoints();
        }
    }

    void AdjustPoints()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.position = new Vector3(child.position.x, transform.position.y + i * spacing, child.position.z);
        }
    }
}
