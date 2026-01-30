using System;
using UnityEngine;

[Serializable]
public struct MarkerData
{
    public string name;
    public float time; // in seconds
}

[CreateAssetMenu(menuName = "FMOD/MarkerDataList")]
public class MarkerDataList : ScriptableObject
{
    public MarkerData[] markers;
}