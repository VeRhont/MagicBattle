using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float _beamLength;
    [SerializeField] private ParticleSystem _impactEffect;

    private LineRenderer _lineRenderer;
    private Transform _startPoint;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPoints(Transform firePoint)
    {
        var hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
        var hitPosition = new Vector2(hitInfo.point.x, hitInfo.point.y);

        if (hitInfo.transform.gameObject.CompareTag("Enemy"))
        {
            Instantiate(_impactEffect, hitPosition, _impactEffect.transform.rotation);
            Attack(hitInfo.transform.gameObject.GetComponent<Enemy>());
        }       

        _lineRenderer.SetPosition(0, firePoint.position);
        _lineRenderer.SetPosition(1, hitPosition);
    }


    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(1);
    }
}
