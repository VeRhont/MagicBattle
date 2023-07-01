using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class YSortDynamic : MonoBehaviour
{
    [SerializeField] private Transform _sortOffsetMarker;

    private SortingGroup _sortingGroup;
    private int _baseSortingOrder;
    private float _ySortingOffset;

    private void Start()
    {
        _sortingGroup = GetComponent<SortingGroup>();
        _ySortingOffset = _sortOffsetMarker.position.y;
    }

    private void Update()
    {
        _baseSortingOrder = transform.GetSortingOrder(_ySortingOffset);
        _sortingGroup.sortingOrder = _baseSortingOrder;
    }
}