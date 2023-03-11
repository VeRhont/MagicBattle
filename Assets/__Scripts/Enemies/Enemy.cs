using UnityEngine;

public enum EnemyType
{
    Zombie,
    Spikey,
    Fly
}

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    [SerializeField] private int _pointsForKill;

    protected Rigidbody2D _enemyRb;
    protected Animator _enemyAnimator;
    protected Transform _playerTransform;
    protected PlayerController _playerController;

    public virtual void Awake()
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

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
