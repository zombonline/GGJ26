using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SongPlayer : MonoBehaviour
{
    public SongChart[] charts;
    public int index = 0;
    public SongChart currentChart => charts[index];

    private AudioSource source;

    public float TrackTime => source.time;
    public float SongLength => charts[index].audioClip.length;
    
    public UnityEvent onSongFinished;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = currentChart.audioClip;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (source.isPlaying)
                source.Pause();
            else
                Play();
        }

        if (Mathf.Approximately(source.time, charts[index].audioClip.length))
        {
            index = (index + 1) % charts.Length;
            source.clip = currentChart.audioClip;
            onSongFinished?.Invoke();
        }
    }

    public void Play()
    {
        source.Play();
    }

    public void Pause()
    {
        source.Pause();
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }
}