using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SongPlayer : MonoBehaviour
{
    public SongChart chart;

    private AudioSource source;

    public float TrackTime => source.time;
    public float SongLength => chart.audioClip.length;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = chart.audioClip;
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
    }

    public void Play()
    {
        source.Play();
    }

    public void Pause()
    {
        source.Pause();
    }
}