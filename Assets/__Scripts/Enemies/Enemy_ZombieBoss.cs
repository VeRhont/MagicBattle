using UnityEngine;
using System.Collections;

public class Enemy_ZombieBoss : Enemy_Zombie
{
    [SerializeField] private float _criticalHpCount;
    [SerializeField] private int _summonCount;
    [SerializeField] private float _summonRange;

    [SerializeField] private Collider2D[] _colliders;

    private bool _canSummon = true;

    protected override void Update()
    {
        if ((CurrentHealth <= _criticalHpCount) && _canSummon)
        {
            StartCoroutine(SummonZombies(_summonCount));
            _canSummon = false;
        }
        base.Update();
    }

    private IEnumerator SummonZombies(int count)
    {
        _enemyAnimator.SetTrigger("Summon");
        DisableMovement();

        for (int i = 0; i < count; i++)
        {
            var spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * _summonRange;
            EnemyFactory.Instance.CreateEnemy(EnemyType.Zombie, spawnPosition);

            yield return new WaitForSeconds(0.6f);
        }
        EnableMovement();
    }

    private void DisableMovement()
    {
        _enemyRb.velocity = Vector2.zero;
        ((Enemy_Zombie)this).enabled = false;
        _colliders[0].enabled = false;
        _colliders[1].enabled = false;
    }

    private void EnableMovement()
    {
        ((Enemy_Zombie)this).enabled = true;
        _colliders[0].enabled = true;
        _colliders[1].enabled = true;
    }
}
