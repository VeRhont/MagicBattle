using UnityEngine;
using System.Collections;

public class Enemy_Zombie : Enemy
{
    [Header("Enemy_Zombie")]
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetweenAttack;

    private bool _canAttack = true;
    private bool _isAttacking = false;

    protected virtual void Update()
    {
        if (_isAttacking && _canAttack)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        var direction = GetDirectionVectorToPlayer();

        _enemyRb.MovePosition(_enemyRb.position + direction * _speed * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        _enemyAnimator.SetTrigger("Attack");
        _playerController.TakeDamage(_attackDamage);

        StartCoroutine(WaitBetweenAttack());
    }

    private IEnumerator WaitBetweenAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_timeBetweenAttack);
        _canAttack = true;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isAttacking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isAttacking = false;
        }
    }
}
