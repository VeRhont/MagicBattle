using UnityEngine;

public class Enemy_Fly : Enemy
{
    [Header("Enemy_Fly")]
    [SerializeField] private float _speed;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _distanceToShoot;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _timeBetweenShots;

    private float _timeFromLastShot;

    private bool isCloseEnough => Vector2.Distance(transform.position, _playerTransform.position) <= _distanceToShoot;

    private void Start()
    {
        _timeFromLastShot = _timeBetweenShots;
    }

    private void Update()
    {
        _timeFromLastShot -= Time.deltaTime;

        if (_timeFromLastShot <= 0 && isCloseEnough)
        {
            Shoot();
            _timeFromLastShot = _timeBetweenShots;
        }
    }

    private void FixedUpdate()
    {
        if (isCloseEnough == false)
        {
            MoveToPlayer();
        }
        else
        {
            FlyAroundPlayer();
        }
    }

    private void MoveToPlayer()
    {
        LookAtPlayer(_playerTransform.position);

        Vector2 playerPosition = _playerTransform.position;
        var direction = (playerPosition - _enemyRb.position).normalized;

        _enemyRb.MovePosition(_enemyRb.position + direction * _speed * Time.fixedDeltaTime);
    }

    private void FlyAroundPlayer()
    {
        Vector2 playerPosition = _playerTransform.position;
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
