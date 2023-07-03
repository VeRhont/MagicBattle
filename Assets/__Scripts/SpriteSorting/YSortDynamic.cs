using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class YSortDynamic : MonoBehaviour
{
    [SerializeField] private Transform _sortOffsetMarker;

    private SortingGroup _sortingGroup;
    private float _ySortingOffset;

    private void Awake()
    {
        _sortingGroup = GetComponent<SortingGroup>();
        _ySortingOffset = _sortOffsetMarker.position.y;
    }

    private void Update()
    {
        _sortingGroup.sortingOrder = transform.GetSortingOrder(_ySortingOffset);
    }
}