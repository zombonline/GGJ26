using System;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField] private Color[] colors;
    private int colorIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void ChangeColor()
    {
        colorIndex = colorIndex + 1 >= colors.Length ? 0 : colorIndex + 1;
        spriteRenderer.color = colors[colorIndex];
    }
}
