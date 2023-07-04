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
    Plant,
    Default
}

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance;

    public Action<int> EnemySpawned;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Enemy[] _enemyPrefabs;

    public void CreateEnemy(EnemyType enemyType, Vector2 position)
    {
        var index = (int)enemyType;
        var enemyPrefab = _enemyPrefabs[index];
        var enemy = Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity);
        enemy.transform.position = position;

        EnemySpawned?.Invoke(enemyPrefab.SpawnPrice);
    }
}
