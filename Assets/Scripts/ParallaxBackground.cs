using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LevelMover levelMover;

    [Header("Components")]
    [SerializeField] private List<ParallaxBackgroundLayer> layers;

    private Vector3 _lastLevelMoverPosition;
    
    private void Awake()
    {
        layers = transform.GetComponentsInChildren<ParallaxBackgroundLayer>().ToList();
    }
    
    private void Update()
    {
        float moveDelta = (levelMover.transform.position.x - _lastLevelMoverPosition.x) * -1f;
        foreach (ParallaxBackgroundLayer layer in layers)
        {
            layer.Move(mainCamera, moveDelta);
        }
        _lastLevelMoverPosition = levelMover.transform.position;
    }
}
