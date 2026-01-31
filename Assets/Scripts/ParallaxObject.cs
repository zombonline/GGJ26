using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParallaxObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 randomRanges;
    
    private Transform[] _recurrentCopies = new Transform[3];
    private Vector3[] _recurrentCopyCentres = new Vector3[3];
    private Vector3[] _recurrentCopyOffsets =  new Vector3[3];
    private float _rightExtend;
    
    private void OnDrawGizmosSelected()
    {
        if (spriteRenderer.sprite == null)
            return;

        Vector2 pivotedOffset = (spriteRenderer.sprite.pivot - spriteRenderer.sprite.textureRect.size / 2f) / spriteRenderer.sprite.pixelsPerUnit * spriteRenderer.transform.lossyScale.x * -1f;
        Vector3 pivotedCentre = transform.position + (Vector3)pivotedOffset;
        Vector3 halfSize = (spriteRenderer.sprite.textureRect.size / spriteRenderer.sprite.pixelsPerUnit + randomRanges) / 2f * spriteRenderer.transform.lossyScale.x;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(pivotedCentre, 0.01f);
        Gizmos.DrawLine(pivotedCentre + new Vector3(-halfSize.x, -halfSize.y, 0f), pivotedCentre + new Vector3(halfSize.x, -halfSize.y, 0f));
        Gizmos.DrawLine(pivotedCentre + new Vector3(-halfSize.x, -halfSize.y, 0f), pivotedCentre + new Vector3(-halfSize.x, halfSize.y, 0f));
        Gizmos.DrawLine(pivotedCentre + new Vector3(halfSize.x, halfSize.y, 0f), pivotedCentre + new Vector3(halfSize.x, -halfSize.y, 0f));
        Gizmos.DrawLine(pivotedCentre + new Vector3(halfSize.x, halfSize.y, 0f), pivotedCentre + new Vector3(-halfSize.x, halfSize.y, 0f));
    }

    public void MakeRecurrentCopy(float layerLength)
    {
        Vector2 pivotedOffset = (spriteRenderer.sprite.pivot - spriteRenderer.sprite.textureRect.size / 2f) / spriteRenderer.sprite.pixelsPerUnit * spriteRenderer.transform.lossyScale.x * -1f;
        Vector3 halfSize = (spriteRenderer.sprite.textureRect.size / spriteRenderer.sprite.pixelsPerUnit + randomRanges) / 2f * spriteRenderer.transform.lossyScale.x;
        _rightExtend = halfSize.x + pivotedOffset.x;
        
        _recurrentCopies[0] = CreateRecurrentCopy(transform.position);
        _recurrentCopies[1] = CreateRecurrentCopy(transform.position - Vector3.right * layerLength);
        _recurrentCopies[2] = CreateRecurrentCopy(transform.position + Vector3.right * layerLength);
        spriteRenderer.enabled = false;
        
        for (int i = 0; i < _recurrentCopies.Length; i++)
        {
            _recurrentCopyCentres[i] = _recurrentCopies[i].position;
            _recurrentCopyOffsets[i] = new Vector3(Random.Range(-0.5f, 0.5f) * randomRanges.x, Random.Range(-0.5f, 0.5f) * randomRanges.y, 0f);
        }
    }

    private Transform CreateRecurrentCopy(Vector3 position)
    {
        GameObject go = new GameObject($"{gameObject.name}(Copy)", typeof(SpriteRenderer));
        go.transform.SetParent(transform);
        go.transform.position = position;
        go.transform.localScale = Vector3.one;
        go.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
        
        return go.transform;
    }
    
    public void Move(Camera mainCamera, float layerLength, float delta)
    {
        for (int i = 0; i < _recurrentCopies.Length; i++)
        {
            _recurrentCopyCentres[i] += Vector3.left * delta;
            if (_recurrentCopyCentres[i].x + _rightExtend < mainCamera.orthographicSize * Screen.width / Screen.height * -1f)
            {
                _recurrentCopyCentres[i] += Vector3.right * layerLength * 3f;
                _recurrentCopyOffsets[i] = new Vector3(Random.Range(-0.5f, 0.5f) * randomRanges.x, Random.Range(-0.5f, 0.5f) * randomRanges.y, 0f);
            }
            _recurrentCopies[i].position = _recurrentCopyCentres[i] + _recurrentCopyOffsets[i];
        }
    }
}
