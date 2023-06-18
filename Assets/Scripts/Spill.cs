using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spill : MonoBehaviour
{
    ParticleSystem _spill;
    [SerializeField] private Bottle bottle;
    // Start is called before the first frame update
    void Start()
    {
        _spill = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Angle(Vector3.down, transform.forward) <= 135 && bottle.IsOpen() )
        {
            _spill.Play();
        }
        else
        {
            _spill.Stop();
        }
    }
}
