using UnityEngine;

public class BlackKnight : Enemy
{
    [SerializeField] private float _distanceToTeleport = 10;
    [SerializeField] private float _timeBetweenDistanceCheck = 1.5f;
    private Collider2D[] _colliders;

    private void OnEnable()
    {
        _colliders = _enemyRb.GetComponentsInParent<Collider2D>();
        InvokeRepeating("CheckDistanceToPlayer", 0, _timeBetweenDistanceCheck);
    }

    private void CheckDistanceToPlayer()
    {
        if (Mathf.Pow(_playerTransform.position.x - transform.position.x, 2) +
            Mathf.Pow(_playerTransform.position.y - transform.position.y, 2) >= Mathf.Pow(_distanceToTeleport, 2) )
        {
            _enemyAnimator.SetTrigger("Teleport");
        }
    }

    public void OnEnableColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
    }

    public void OnDisableColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }

    public void OnStop()
    {
        _enemyRb.velocity = Vector2.zero;
    }

    public void OnResetTriggers()
    {
        _enemyAnimator.ResetTrigger("FlightAttack");
        _enemyAnimator.ResetTrigger("RightAttack");
        _enemyAnimator.ResetTrigger("LeftAttack");
        _enemyAnimator.ResetTrigger("Defence");
        _enemyAnimator.ResetTrigger("Teleport");
        _enemyAnimator.ResetTrigger("DestroyObstacles");
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (Random.value < 0.1f)
        {
            _enemyAnimator.SetTrigger("Defence");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_playerTransform.position.x > transform.position.x)
            {
                _enemyAnimator.SetTrigger("RightAttack");
            }
            else
            {
                _enemyAnimator.SetTrigger("LeftAttack");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enviroment"))
        {
            _enemyAnimator.SetTrigger("DestroyObstacles");
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemyAnimator.SetTrigger("DestroyObstacles");
        }
    }
}
