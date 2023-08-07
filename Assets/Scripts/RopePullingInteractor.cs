using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using System.Linq;

public class RopePullingInteractor : MonoBehaviour
{
    [SerializeField] private GameObject _grabbingPoint;

    private Vector3 startPos;
    private float baseLength;

    private void Start()
    {
        startPos = _grabbingPoint.transform.position;
        baseLength = transform.localScale.y;
    }

    void Update()
    {
        if( _grabbingPoint is null)
        {
            return;
        }

        float scaleChange = (_grabbingPoint.transform.position - startPos).y;
        transform.localScale = new Vector3 (transform.localScale.x, Mathf.Max(baseLength, baseLength - scaleChange), transform.localScale.z);
    }
}