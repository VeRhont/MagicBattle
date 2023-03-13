using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Transform _playerPosition;
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

        if (_spawnRange >= _rightBound - _leftBound)
        {
            _spawnRange = (_rightBound - _leftBound) / 2;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnZombies(5));
    }

    private void Update()
    {
        if (_aliveEnemiesCount <= 0)
        {
            SpawnZombies(_waveNumber);
        }
    }

    private IEnumerator SpawnZombies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _aliveEnemiesCount++;

            var spawnPosition = GetPosition();
            _enemyFactory.CreateEnemy(EnemyType.Zombie, spawnPosition);

            yield return new WaitForSeconds(Random.Range(0, _timeBetweenSpawn));
        }
    }

    private Vector2 GetPosition()
    {
        var playerPosition = _playerPosition.transform.position;
        var generatedPosition = new Vector2();

        do {
            var angle = Random.Range(0, 360);
            var x = _spawnRange * Mathf.Cos(angle);
            var y = _spawnRange * Mathf.Sin(angle);

            generatedPosition = new Vector2(x, y);
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
