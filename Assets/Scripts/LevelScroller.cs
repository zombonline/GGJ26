using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public SongPlayer songPlayer;
    public float unitsPerSecond = 5f;
    [SerializeField] private Vector2 targetPos;

    void Update()
    {
        float x = -songPlayer.TrackTime * unitsPerSecond;
        transform.position = new Vector3(targetPos.x + x, targetPos.y, 0f);
    }
}