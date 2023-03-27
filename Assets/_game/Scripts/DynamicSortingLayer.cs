using UnityEngine;

public class DynamicSortingLayer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _spriteRenderer.sortingOrder = (int)(transform.position.z * -100);
    }
}
