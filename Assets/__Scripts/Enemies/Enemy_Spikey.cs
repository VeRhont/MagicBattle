using UnityEngine;
using System.Collections;

public class Enemy_Spikey : Enemy
{
    [Header("Enemy_Spikey")]
    [SerializeField] private float _dashStrength;
    [SerializeField] private float _timeBetweenAttack;
    [SerializeField] private float _attackTime;

    private float _timeFromLastAttack = 0;

    private void Start()
    {
        _timeFromLastAttack = _timeBetweenAttack;
    }

    private void Update()
    {
        _timeFromLastAttack -= Time.deltaTime;

        if (_timeFromLastAttack <= 0)
        {
            StartCoroutine(Attack());
            _timeFromLastAttack = _timeBetweenAttack;
        }
    }

    private IEnumerator Attack()
    {
        Stop();
        _enemyAnimator.SetBool("IsRolling", true);

        Vector2 playerPosition = _playerTransform.position;
        var direction = (playerPosition - _enemyRb.position).normalized;
        _enemyRb.AddForce(direction * _dashStrength, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_attackTime);

        _enemyAnimator.SetBool("IsRolling", false);
        Stop();
    }

    private void Stop()
    {
        _enemyRb.velocity = Vector2.zero;
    }
}
