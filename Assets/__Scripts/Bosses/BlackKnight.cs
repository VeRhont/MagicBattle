using UnityEngine;

public class BlackKnight : Enemy
{
    private void Update()
    {
        if (Random.value < 0.05f)
        {
            _enemyAnimator.SetTrigger("FlightAttack");
        }
    }

    public override void TakeDamage(float damage)
    {
        if (Random.value < 0.2f)
        {
            _enemyAnimator.SetTrigger("Defence");
        }
        base.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _enemyAnimator.SetTrigger("Attack");
        }
    }
}
