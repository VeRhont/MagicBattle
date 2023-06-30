using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float _beamLength = 100f;
    [SerializeField] private ParticleSystem _impactEffect;

    private LineRenderer _lineRenderer;
    private Transform _startPoint;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPoints(Transform firePoint, int damage)
    {
        var hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);

        if (hitInfo.collider != null)
        {
            var hitPosition = new Vector2(hitInfo.point.x, hitInfo.point.y);

            Instantiate(_impactEffect, hitPosition, _impactEffect.transform.rotation);

            if (hitInfo.transform.gameObject.CompareTag("Enemy"))
            {
                Attack(hitInfo.transform.gameObject.GetComponent<Enemy>(), damage);
            }       

            _lineRenderer.SetPosition(0, firePoint.position);
            _lineRenderer.SetPosition(1, hitPosition);
        }
    }

    private void Attack(Enemy enemy, int damage)
    {
        enemy.TakeDamage(damage);
    }
}
