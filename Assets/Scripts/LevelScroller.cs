using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public SongPlayer songPlayer;
    public float unitsPerSecond = 5f;

    void Update()
    {
        float x = -songPlayer.TrackTime * unitsPerSecond;
        transform.position = new Vector3(x, 0f, 0f);
    }
}