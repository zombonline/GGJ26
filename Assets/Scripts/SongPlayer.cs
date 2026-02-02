using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SongPlayer : MonoBehaviour
{
    public SongChart[] charts;
    public int index = 0;
    public float songEndDetectionMargin;
    
    public SongChart currentChart => charts[index];

    private AudioSource source;

    public float TrackTime => source.time;
    public float SongLength => charts[index].audioClip.length;

    public int SongIndex => index;
    
    public UnityEvent onSongFinished;

    public UnityEvent onGameFinished;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = currentChart.audioClip;
    }

    private void Update()
    {
        if (index < charts.Length)
        {
            if (source.isPlaying && charts[index].audioClip.length - source.time <= songEndDetectionMargin)
            {
                index++;
                if (index < charts.Length)
                {
                    source.Pause();
                    source.clip = currentChart.audioClip;
                    onSongFinished?.Invoke();
                }
                else
                {
                    source.Pause();
                    onGameFinished?.Invoke();
                }
            }
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