using UnityEngine;

public class Enemy_Fly : Enemy
{
    [Header("Enemy_Fly")]
    [SerializeField] private float _speed;

    [SerializeField] protected Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private float _distanceToShoot;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _timeBetweenShots;

    protected float _timeFromLastShot;

    private bool isCloseEnough => (distanceToPlayer <= _distanceToShoot);

    private void Start()
    {
        _timeFromLastShot = _timeBetweenShots;
    }

    protected virtual void Update()
    {
        _timeFromLastShot -= Time.deltaTime;

        if ((_timeFromLastShot <= 0) && isCloseEnough)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (isCloseEnough)
        {
            FlyAroundPlayer();       
        }
        else
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        LookAtPlayer(_playerTransform.position);

        var direction = GetDirectionVectorToPlayer();
        _enemyRb.MovePosition(_enemyRb.position + direction * _speed * Time.fixedDeltaTime);
    }

    private void FlyAroundPlayer()
    {
        _enemyRb.velocity = Vector2.zero;

        var playerPosition = _playerTransform.position;
        LookAtPlayer(playerPosition);
    }

    private void LookAtPlayer(Vector2 playerPosition)
    {
        var direction = (playerPosition - _enemyRb.position).normalized;

        var lookDirection = playerPosition - _enemyRb.position;
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        _enemyRb.rotation = angle;
    }

    protected virtual void Shoot()
    {
        _timeFromLastShot = _timeBetweenShots;

        _enemyAnimator.SetTrigger("Attack");
        var bullet = Instantiate<GameObject>(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }
}
