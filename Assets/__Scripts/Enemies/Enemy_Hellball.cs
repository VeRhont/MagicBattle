using UnityEngine;

public class Enemy_Hellball : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDistance;

    private void Update()
    {
        if (distanceToPlayer > _attackDistance)
        {
            MoveToPlayer();
        }
        else
        {
            Attack();
        }
    }

    private void MoveToPlayer()
    {
        var direction = GetDirectionVectorToPlayer();

        _enemyRb.MovePosition(_enemyRb.position + direction * _speed * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        var direction = GetDirectionVectorToPlayer();
        _enemyRb.AddForce(direction * _attackForce, ForceMode2D.Impulse);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
            base.OnCollisionEnter2D(collision);
        }
    }
}
