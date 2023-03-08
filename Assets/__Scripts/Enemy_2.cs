using UnityEngine;
using System.Collections;

public class Enemy_2 : Enemy
{
    [Header("Enemy_2")]
    [SerializeField] private float _dashStrength;
    [SerializeField] private float _timeBetweenAttack = 2f;
    [SerializeField] private float _attackTime = 2f;

    private float _lastAttackTime = 0;

    private void Update()
    {
        _lastAttackTime -= Time.deltaTime;

        if (_lastAttackTime <= 0)
        {
            StartCoroutine(Attack());

            _lastAttackTime = _timeBetweenAttack;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator Attack()
    {
        Debug.Log(Time.time);

        _enemyRb.velocity = Vector2.zero;

        _enemyAnimator.SetBool("IsRolling", true);

        Vector2 playerPosition = playerTransform.position;
        var direction = (playerPosition - _enemyRb.position).normalized;

        _enemyRb.AddForce(direction * _dashStrength, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_attackTime);

        _enemyAnimator.SetBool("IsRolling", false);
    }
}