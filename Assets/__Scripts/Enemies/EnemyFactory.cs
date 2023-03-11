using UnityEngine;
using System;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;

    public void CreateEnemy(EnemyType enemyType, Vector2 position)
    {
        switch (enemyType)
        {
            case EnemyType.Zombie:
                Instantiate(_enemyPrefabs[0], position, Quaternion.identity);
                break;

            case EnemyType.Spikey:
                Instantiate(_enemyPrefabs[1], position, Quaternion.identity);
                break;

            case EnemyType.Fly:
                Instantiate(_enemyPrefabs[2], position, Quaternion.identity);
                break;

            default:
                throw new ArgumentException("Unknown enemy type");
        }
    }
}
