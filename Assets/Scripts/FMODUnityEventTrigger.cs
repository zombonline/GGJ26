using System;
using System.Net.Sockets;
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
    private volatile string lastMarkerName = "";

    void Start()
    {
        FMODUnity.RuntimeManager.LoadBank("Main", true); // load synchronously
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (instance.isValid())
            {
                instance.setCallback(null, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

                // stop and release the instance
                instance.stop(STOP_MODE.IMMEDIATE);
                instance.release();
            }
            else
            {
                instance = RuntimeManager.CreateInstance(fmodEvent);

                // register callback
                instance.setCallback(BeatCallback);
                instance.start();
            }
        }
        
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
        if (shuttingDown)
            return RESULT.OK;

        switch (type)
        {
            case EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                beatTriggered = true;

                var info = (TIMELINE_MARKER_PROPERTIES)System.Runtime.InteropServices.Marshal.PtrToStructure(
                    parameters, typeof(TIMELINE_MARKER_PROPERTIES));
                lastMarkerName = info.name;
                break;

            case EVENT_CALLBACK_TYPE.STOPPED:
                if (instance.isValid())
                {
                    instance.release();
                    instance = default;
                }
                break;
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