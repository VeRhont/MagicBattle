using UnityEngine;

public class Enemy_Slime : Enemy
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToAttack;

    [SerializeField] private GameObject[] _slimes;

    private bool isCloseEnough => (distanceToPlayer <= _distanceToAttack);

    private void FixedUpdate()
    {
        if (isCloseEnough)
        {
            Attack();
        }
        else
        {
            MoveToPlayer();
        }
    }

    private void Attack()
    {
        var playerPosition = _playerTransform.position;

    }

    private void MoveToPlayer()
    {
        _enemyAnimator.SetBool("IsMoving", true);
        
        Vector2 direction = GetDirectionVectorToPlayer();
        _enemyRb.MovePosition(_enemyRb.position + direction *_speed * Time.deltaTime);
    }

    private void Stop()
    {
        _enemyAnimator.SetBool("IsMoving", false);
    }
}
