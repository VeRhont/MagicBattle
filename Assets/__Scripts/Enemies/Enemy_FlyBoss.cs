using UnityEngine;
using System.Collections;

public class Enemy_FlyBoss : Enemy_Fly
{
    [Header("Enemy_FlyBoss")]
    [SerializeField] private GameObject _fastBulletPrefab;

    [SerializeField, Range(0, 1)] private float _commonBulletChanceValue = 0.6f;
    [SerializeField, Range(0, 1)] private float _burstChanceValue = 0.8f;
    [SerializeField, Range(0, 1)] private float _spreadChanceValue = 1f;

    [SerializeField] private int _burstBulletsCount = 5;
    [SerializeField] private float _timeBetweenBurst = 3f;

    [SerializeField] private int _spreadBulletsCount = 5;
    [SerializeField] private float _initialAngle = -30f;

    private IEnumerator FireBurst()
    {
        _timeFromLastShot = _timeBetweenBurst;

        for (int i = 0; i < _burstBulletsCount; i++)
        {
            Instantiate(_fastBulletPrefab, _firePoint.position, _firePoint.rotation);

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FireSpreadBullets()
    {
        _timeFromLastShot = 2f;

        var angle = _initialAngle;
        var delta = Mathf.Abs(_initialAngle) * 2 / _spreadBulletsCount;

        for (int i = 0; i < _spreadBulletsCount; i++)
        {
            var bullet = Instantiate(_fastBulletPrefab, _firePoint.position, _firePoint.rotation);

            var rotation = bullet.transform.rotation;
            rotation.z += angle;

            bullet.transform.rotation = rotation;

            angle += delta;
        }
    }

    protected override void Shoot()
    {
        var randomValue = Random.value;

        if (randomValue < _commonBulletChanceValue)
        {
            base.Shoot();
        }
        else if (randomValue < _burstChanceValue)
        {
            StartCoroutine(FireBurst());
        }
        else
        {
            FireSpreadBullets();
        }
    }
}
