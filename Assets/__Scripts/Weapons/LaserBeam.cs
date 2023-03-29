using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float _beamLength;

    private LineRenderer _lineRenderer;
    private Transform _startPoint;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPoints(Transform firePoint)
    {
        _lineRenderer.SetPosition(0, firePoint.position);

        var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);       
       
        _lineRenderer.SetPosition(1, direction * 10);
    }
}
