using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class AudioManager : Singleton<AudioManager>
{
    public FMODUnity.EventReference[] eventReferences;
    private List<FMOD.Studio.EventInstance> _eventInstances;

    public enum Sounds
    {
        // ADD SOUNDS NAME HERE IN THE SAME ORDER THAT YOU PUT IN THE eventReferences 
        // Add name of events here such as :
        // FALL
        // TELEPORT etc etc
        
        i_just_had,
        
        i_should,
        
        ay_cap,
        
        avast,
        
        pfft,

        steps,

        bottle_up,

        bottle_down,

        bottle_open,

        bottle_close,

        water_drinking,


        ay_leut,

        ye_two,

        much_obliged,

        ye_sea_rats,

        rope_pull_fx,

        but_cap_said,

        fine_work,

        them_black_clouds,

        right_maggots,

        ay_sir_right_maggots,

        headcount_the_maggots,
        
        ay_sir_headcount,

        benji,

        ay_benji,

        montague,

        ay_montague,

        george,

        ay_george,

        will_john,

        big_wave,

        that_was_not,

        will_john_scream,

        whale_crash_end,


    }

    protected override void Awake()
    {
        base.Awake();
        _eventInstances = new List<EventInstance>();
        foreach (var fmodEvent in eventReferences)
        {
            EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent.Guid);
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            _eventInstances.Add(instance);
        }
    }
    /// <summary>
    /// Playing sound from the player
    /// </summary>
    /// <param name="sound"></param>
    /// <returns></returns>
    public EventInstance PlaySound(Sounds sound)
    {
        _eventInstances[(int)sound].start();
        _eventInstances[(int)sound].set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        return _eventInstances[(int)sound];
    }

    public EventInstance PlaySound(Sounds sound, GameObject obj) {
        _eventInstances[(int)sound].start();
        _eventInstances[(int)sound].set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(obj.transform));
        return _eventInstances[(int)sound];
    }


    public void StopSound(Sounds sound)
    {
        _eventInstances[(int)sound].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        // _eventInstances[(int)sound].release(); // releasing will kill the event
    }
    /// <summary>
    /// Playing sound from the player for one burst
    /// </summary>
    /// <param name="sound"></param>
    public void PlayOneShot(Sounds sound)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventReferences[(int)sound]);
    }
    /// <summary>
    /// Playing sound from the game object
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="attach"></param>
    public EventInstance PlayOneShotAttach(Sounds sound, GameObject attach)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventReferences[(int)sound], attach);
        return _eventInstances[(int)sound];
    }

    public bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    public bool IsPlaying(Sounds sound) {
        FMOD.Studio.PLAYBACK_STATE state;
        FMOD.Studio.EventInstance instance = GetSoundEventInstance(sound);
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    public IEnumerator WaitForSoundToFinish(EventInstance instance)
    {
        while (AudioManager.Instance.IsPlaying(instance))
        {
            yield return null;
        }
    }

    public EventInstance GetSoundEventInstance(Sounds sound)
    {
        return _eventInstances[(int)sound];
    }

    /// <summary>
    /// Use this when an object has an event Emitter attached to it.
    /// </summary>
    /// <param name="obj"></param>
    public void PlayFmodEventEmitter(GameObject obj)
    {
        StudioEventEmitter eventEmitter = obj.GetComponent<StudioEventEmitter>();
        if (!eventEmitter)
        {
            Debug.Log($"Event Emitter is not found in game object {obj.name}");
            return;
        }

        eventEmitter.Play();
    }

    public void SetParameter(string parameterName, float value, Sounds sound)
    {
        _eventInstances[(int)sound].setParameterByName(parameterName, value);
    }

    public float GetParameter(string parameterName, Sounds sound)
    {
        float value;
        _eventInstances[(int)sound].getParameterByName(parameterName, out value);
        return value;
    }
}
