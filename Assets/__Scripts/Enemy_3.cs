using UnityEngine;

public class Enemy_3 : Enemy
{
    [Header("Enemy_3")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _bulletForce;
    [SerializeField] private float _timeBetweenShots;

    private float _lastShotTime;

    private void Start()
    {
        _lastShotTime = _timeBetweenShots;
    }

    private void Update()
    {
        _lastShotTime -= Time.deltaTime;

        if (_lastShotTime <= 0)
        {
            Shoot();

            _lastShotTime = _timeBetweenShots;
        }
    }

    private void FixedUpdate()
    {
        Vector2 playerPosition = playerTransform.position;
        Vector2 direction = transform.up + transform.right;

        _enemyRb.MovePosition(_enemyRb.position + direction * _speed * Time.fixedDeltaTime);

        LookAtPlayer(playerPosition);
    }

    private void LookAtPlayer(Vector2 playerPosition)
    {       
        var direction = (playerPosition - _enemyRb.position).normalized;

        var lookDirection = playerPosition - _enemyRb.position;
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        _enemyRb.rotation = angle;
    }

    private void Shoot()
    {
        _enemyAnimator.SetTrigger("Attack");

        var bullet = Instantiate<GameObject>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletRb.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
    }
}