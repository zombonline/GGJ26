using System;
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



    private void Update()
    {
        if(transform.position.x < -10) 
            gameObject.SetActive(false);
    }

    public void ReactToPlayerInteraction(ObstacleSuccessState successState)
    {
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
