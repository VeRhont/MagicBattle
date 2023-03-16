using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _timeBetweenSpawn;

    private int _waveNumber = 1;
    private int _aliveEnemiesCount = 0;

    [Header("Coordinates")]
    [SerializeField] private Transform _leftUpCorner;
    [SerializeField] private Transform _rightDownCorner;

    private float _rightBound;
    private float _leftBound;
    private float _upBound;
    private float _downBound;

    private void Awake()
    {
        _rightBound = _rightDownCorner.position.x;
        _leftBound = _leftUpCorner.position.x;
        _downBound = _rightDownCorner.position.y;
        _upBound = _leftUpCorner.position.y;
    }

    private IEnumerator SpawnEnemyWave(EnemyType enemyType, int count, float spawnRadius)
    {
        for (int i = 0; i < count; i++)
        {
            _aliveEnemiesCount++;

            var spawnPosition = GetPosition(spawnRadius);
            _enemyFactory.CreateEnemy(enemyType, spawnPosition);

            yield return new WaitForSeconds(Random.Range(0, _timeBetweenSpawn));
        }
    }

    private Vector2 GetPosition(float spawnRadius)
    {
        if (spawnRadius >= (_rightBound - _leftBound) / 2)
        {
            spawnRadius = (_rightBound - _leftBound) / 2 - 1;
        }

        var playerPosition = _playerTransform.transform.position;
        var generatedPosition = new Vector2();

        do {
            var angle = Random.Range(0, 360);
            var x = spawnRadius * Mathf.Cos(angle);
            var y = spawnRadius * Mathf.Sin(angle);

            generatedPosition = new Vector2(playerPosition.x + x, playerPosition.y + y);
        }
        while (IsInsideBounds(generatedPosition) == false);

        return generatedPosition;
    }

    private bool IsInsideBounds(Vector2 position)
    {
        var xBound = (_leftBound < position.x) && (position.x < _rightBound);
        var yBound = (_downBound < position.y) && (position.y < _upBound);
        return xBound && yBound;
    }
}
