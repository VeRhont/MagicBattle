using UnityEngine;
using System;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;

    public void CreateEnemy(EnemyType enemyType, Vector2 position)
    {
        var index = (int)enemyType;
        Instantiate(_enemyPrefabs[index], position, Quaternion.identity);
    }
}
