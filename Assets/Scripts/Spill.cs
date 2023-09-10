using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spill : MonoBehaviour
{
    ParticleSystem _spill;
    [SerializeField] private Bottle bottle;
    [SerializeField] private int angleToStartSpilling = 135;

    private List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

    private AudioManager audioManager;

    private bool playing = false;
    
    
    void Awake() {
        audioManager = GameObject.Find("Avatar").GetComponent<AudioManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _spill = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (audioManager && playing && !audioManager.IsPlaying(AudioManager.Sounds.water_drinking)) {
            audioManager.PlaySound(AudioManager.Sounds.water_drinking);
        }
        
        if (Vector3.Angle(Vector3.down, transform.forward) <= angleToStartSpilling && bottle.IsOpen())
        {
            playing = true;
            _spill.Play();
        }
        else
        {
            playing = false;;
            _spill.Stop();
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(_spill, other, CollisionEvents);

        for (int i = 0; i < CollisionEvents.Count; i++)
        {
            if (other.name.Contains("Head"))
            {
                bottle.Drink();
            }
            // If anything else - stop at that position - Splash
            Debug.Log(other.name);
        }
    }

   
}
