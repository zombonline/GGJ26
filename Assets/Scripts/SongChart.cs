using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rhythm/Song Chart")]
public class SongChart : ScriptableObject
{
    public AudioClip audioClip;

    public List<Marker> markers = new();

    [Serializable]
    public struct Marker
    {
        public float time;          // seconds into the song
        public GameObject obstacle;
    }
}