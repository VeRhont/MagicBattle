using UnityEngine;

public class Enemy_Zombie : Enemy
{
    [Header("Enemy_Zombie")]
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector2 playerPosition = _playerTransform.position;
        var direction = (playerPosition - _enemyRb.position).normalized;

        _enemyRb.MovePosition(_enemyRb.position + direction * _speed * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        _enemyAnimator.SetTrigger("Attack");
        _playerController.TakeDamage(_damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
}
