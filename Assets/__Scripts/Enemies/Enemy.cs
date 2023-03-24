using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] protected float _health;
    [SerializeField] protected float _attackDamage;
    [SerializeField] protected float _contactDamage;
    [SerializeField] private int _pointsForKill;

    protected Rigidbody2D _enemyRb;
    protected Animator _enemyAnimator;
    protected Transform _playerTransform;
    protected PlayerController _playerController;

    protected float distanceToPlayer => Vector2.Distance(transform.position, _playerTransform.position);

    protected virtual void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponentInChildren<Animator>();

        var player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = player.transform;
        _playerController = player.GetComponent<PlayerController>();
    }

    public virtual void TakeDamage(float damage)
    {
        _health -= damage;
        DamageUI.Instance.AddText((int)damage, transform.position);

        if (_health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        StopAllCoroutines();

        EnemySpawnManager.Instance.DecreaseAliveEnemiesCount();
        Destroy(gameObject);
    }

    protected Vector2 GetDirectionVectorToPlayer()
    {
        Vector2 playerPosition = _playerTransform.position;
        var direction = (playerPosition - _enemyRb.position).normalized;

        return direction;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.TakeDamage(_contactDamage);
        }
    }
}
