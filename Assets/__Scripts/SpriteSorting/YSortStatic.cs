using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSortStatic : MonoBehaviour
{
    private void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = transform.GetSortingOrder();

        Destroy(this);
    }
}