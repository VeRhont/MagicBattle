using UnityEngine;
using System;

public enum EnemyType
{
    Zombie,
    Fly,
    Spikey,
    Slime,
    ZombieBoss,
    FlyBoss,
    SpikeyBoss,
    Default
}

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;

    public void CreateEnemy(EnemyType enemyType, Vector2 position)
    {
        EnemySpawnManager.Instance.IncreaseAliveEnemiesCount();

        var index = (int)enemyType;
        Instantiate(_enemyPrefabs[index], position, Quaternion.identity);
    }
}
