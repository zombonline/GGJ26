using System;
using FMOD;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class FMODUnityEventTrigger : MonoBehaviour
{
    [Header("FMOD Event")]
    public EventReference fmodEvent;

    [Header("Called every beat")]
    public UnityEvent onBeat;

    private EventInstance instance;

    // Set from FMOD audio thread, read on Unity main thread
    private volatile bool beatTriggered = false;
    private volatile bool shuttingDown = false;

    void Start()
    {
        instance = RuntimeManager.CreateInstance(fmodEvent);

        // register callback
        instance.setCallback(BeatCallback, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        instance.start();
    }

    void Update()
    {
        if (beatTriggered)
        {
            beatTriggered = false;
            Debug.Log("Beat!");
            onBeat?.Invoke();
        }
    }

    // Runs on FMOD's audio thread – NO Unity API calls here
    private RESULT BeatCallback(EVENT_CALLBACK_TYPE type, IntPtr eventPtr, IntPtr parameters)
    {
        // ignore callbacks during/after shutdown
        if (shuttingDown)
            return RESULT.OK;

        if (type == EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
        {
            beatTriggered = true;
        }

        return RESULT.OK;
    }

    void OnDestroy()
    {
        shuttingDown = true;

        if (instance.isValid())
        {
            // remove callback first so FMOD won't call back into this object
            instance.setCallback(null, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

            // stop and release the instance
            instance.stop(STOP_MODE.IMMEDIATE);
            instance.release();
        }
    }
}