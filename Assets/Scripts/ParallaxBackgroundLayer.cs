using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParallaxBackgroundLayer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<ParallaxObject> parallaxObjects;
    
    [Header("Settings")]
    [SerializeField] private float contentLength;
    [SerializeField] private float speedMultiplier;

    private void Awake()
    {
        parallaxObjects = transform.GetComponentsInChildren<ParallaxObject>().ToList();
        foreach (ParallaxObject parallaxObject in parallaxObjects)
        {
            parallaxObject.MakeRecurrentCopy(contentLength);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 left = transform.position + Vector3.left * contentLength / 2f;
        Vector3 right = transform.position + Vector3.right * contentLength / 2f;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(left, right);
        Gizmos.DrawLine(left, left + new Vector3(0.1f, -0.1f, 0f));
        Gizmos.DrawLine(left, left + new Vector3(0.1f, 0.1f, 0f));
        Gizmos.DrawLine(right, right + new Vector3(-0.1f, -0.1f, 0f));
        Gizmos.DrawLine(right, right + new Vector3(-0.1f, 0.1f, 0f));
    }

    public void Move(Camera mainCamera, float delta)
    {
        foreach (ParallaxObject parallaxObject in parallaxObjects)
        {
            parallaxObject.Move(mainCamera, contentLength, delta * speedMultiplier);   
        }
    }
}
