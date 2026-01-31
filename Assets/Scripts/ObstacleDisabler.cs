using System;
using UnityEngine;

public enum BeatHitType
{
    Success,
    Fail
}
public class Obstacle : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite readySprite, hitSprite;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = readySprite;
    }

    private void Update()
    {
        if(transform.position.x < -10) 
            gameObject.SetActive(false);
    }

    public void ReactToPlayerInteraction(BeatHitType type)
    {
        if (type == BeatHitType.Fail)
        {
            
        }
        else if (type == BeatHitType.Success)
        {
            spriteRenderer.sprite = hitSprite;;
        }
    }
}
