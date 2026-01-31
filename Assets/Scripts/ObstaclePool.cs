using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Vector2 playerPos;

    private LevelMover levelMover;

    void Awake()
    {
        levelMover = FindAnyObjectByType<LevelMover>();
    }

    public void ActivateObstacle(int beatsToReachPlayer = 4)
    {
        if (levelMover == null) return;

        // total time until obstacle should reach player
        float travelTime = levelMover.secondsPerBeat * beatsToReachPlayer;
        Debug.Log("Travel time: " + travelTime + "");

        // spawn offset so obstacle reaches player in exactly travelTime seconds
        float spawnOffset = levelMover.speed * travelTime;

        // spawn position
        Vector2 spawnPos = playerPos + new Vector2(spawnOffset, 0);

        // find an inactive obstacle
        GameObject obstacle = obstacles.Find(o => !o.activeInHierarchy);

        if (obstacle == null)
        {
            obstacle = Instantiate(obstaclePrefab, transform);
            obstacles.Add(obstacle);
        }

        // set position and parent to levelMover so it moves along automatically
        obstacle.transform.position = spawnPos;
        obstacle.transform.SetParent(levelMover.transform, worldPositionStays: true);
        obstacle.SetActive(true);
    }

    private void Update()
    {
        
    }
}