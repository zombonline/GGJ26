using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform levelMoverTransform;

    [Header("Components")]
    [SerializeField] private List<ParallaxBackgroundLayer> layers;

    private Vector3 _lastLevelMoverPosition;
    
    private void Awake()
    {
        layers = transform.GetComponentsInChildren<ParallaxBackgroundLayer>().ToList();
    }
    
    private void Update()
    {
        float moveDelta = (levelMoverTransform.transform.position.x - _lastLevelMoverPosition.x) * -1f;
        _lastLevelMoverPosition = levelMoverTransform.transform.position;
        if(Mathf.Abs(moveDelta) > 100 )
            return;
        foreach (ParallaxBackgroundLayer layer in layers)
        {
            layer.Move(mainCamera, moveDelta);
        }
    }
}
