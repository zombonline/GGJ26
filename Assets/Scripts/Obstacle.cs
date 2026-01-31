using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum ObstacleSuccessState
{
    Success,
    Fail
}
public class Obstacle : MonoBehaviour
{
    [SerializeField] public BeatHitType type;
    [SerializeField] public UnityEvent onObstacleHit, onObstacleFail;
    [SerializeField] private BoxCollider2D collider;
    private string name;
    
    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        
    }

    private void OnEnable()
    {
        collider.enabled = true;
    }




    private void Update()
    {
        if(transform.position.x < -10) 
            gameObject.SetActive(false);
    }

    public void ReactToPlayerInteraction(ObstacleSuccessState successState)
    {
        collider.enabled = false;
        if (successState == ObstacleSuccessState.Fail)
        {
            onObstacleFail?.Invoke();   
        }
        else if (successState == ObstacleSuccessState.Success)
        {
            onObstacleHit?.Invoke();
        }
    }
}
