using UnityEngine;

public class Enemy_SpikeyBoss : Enemy_Spikey
{
    [SerializeField] private GameObject[] _crystalBullets;
    [SerializeField] private int _count;

    protected override void Die()
    {
        SpawnCrystals();

        base.Die();
    }

    private void SpawnCrystals()
    {
        var delta = 360 / _count;
        var angle = 0f;

        for (int i = 0; i < _count; i++)
        {
            var random = Random.Range(0, _crystalBullets.Length);
            Instantiate(_crystalBullets[random], transform.position, Quaternion.Euler(0, 0, angle));

            angle += delta;
        }
    }
}
