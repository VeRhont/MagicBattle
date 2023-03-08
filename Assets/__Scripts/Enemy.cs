using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _collisionDamage;

    [SerializeField] protected Animator _enemyAnimator;

    protected Transform playerTransform;
    protected PlayerController _playerController;
    protected Rigidbody2D _enemyRb;

    private float _health;

    [SerializeField] private int _pointsCount;
    public int PointsCount { get { return _pointsCount; }  }

    private void Awake()
    {
        _enemyRb = GetComponent<Rigidbody2D>();

        _health = _maxHealth;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        Vector2 playerPosition = playerTransform.position;
        var direction = (playerPosition - _enemyRb.position).normalized;

        _enemyRb.MovePosition(_enemyRb.position + direction * _speed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        DamageUI.Instance.AddText((int)damage, transform.position);

        if (_health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        _playerController.UpdateScore(_pointsCount);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _enemyAnimator.SetTrigger("Attack");
          
            _playerController.TakeDamage(_collisionDamage);
        }
    }
}