using UnityEngine;
using System;

public enum EnemyType
{
    Zombie,
    Fly,
    Spikey,
    Slime,
    Hellball,
    Wormholl,
    ZombieBoss,
    FlyBoss,
    SpikeyBoss,
    Default
}

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Enemy[] _enemyPrefabs;

    public void CreateEnemy(EnemyType enemyType, Vector2 position)
    {
        EnemySpawnManager.Instance.AliveEnemiesCount += 1;

        var index = (int)enemyType;
        Instantiate(_enemyPrefabs[index], position, Quaternion.identity);
    }
}
