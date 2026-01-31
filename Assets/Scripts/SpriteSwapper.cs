using System;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private Sprite spr1, spr2;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(int index) => spriteRenderer.sprite = (index == 0 ? spr1 : spr2);
}
