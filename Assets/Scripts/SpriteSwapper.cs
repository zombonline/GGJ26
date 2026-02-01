using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (sprites.Length > 0)
        {
            var randomSprite = sprites[Random.Range(0, sprites.Length)];
            Debug.Log($"Setting sprite to {randomSprite}");
            spriteRenderer.sprite = randomSprite;
        }
    }
}
