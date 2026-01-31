using UnityEngine;

[ExecuteAlways]
public class ChartSpawner : MonoBehaviour
{
    public SongChart chart;
    public float unitsPerSecond = 5f;
    public float timeOffset = 0f;
    [ContextMenu("Rebuild Markers")]
    public void RebuildButton()
    {
        Rebuild();
    }
    
    public void Rebuild()
    {
        ClearExistingMarkers();

        // spawn markers from chart
        
        
        foreach (var marker in chart.markers)
        {
            SpawnAtTime(marker.obstacle ,marker.time + timeOffset, $"Marker_{marker.time:F2}s");
        }
    }

    void SpawnAtTime(GameObject spawnObject,float time, string name)
    {
        float x = time * unitsPerSecond;

        var obj = Instantiate(spawnObject, transform);
        obj.name = name;
        obj.transform.localPosition = new Vector3(x, 0f, 0f);
    }

    void ClearExistingMarkers()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i).gameObject;
            Debug.Log($"Destroying {child.name}");
            DestroyImmediate(child);
        }
    }
}