using UnityEngine;

public class LevelMover : MonoBehaviour
{
    public float bpm = 120f;  
    public float distancePerBeat = 2f;   

    [HideInInspector] public float secondsPerBeat;
    [HideInInspector] public float speed; 

    void Awake()
    {
        secondsPerBeat = 60f / bpm;  
        speed = distancePerBeat / secondsPerBeat;  
    }

    void Update()
    {
        // move the level continuously
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}