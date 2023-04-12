using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _timeBetweenSpawn;

    [Header("SpawnChances; simple enemies")]
    [SerializeField, Range(0, 1)] private float _zombieSpawn;
    [SerializeField, Range(0, 1)] private float _flySpawn;
    [SerializeField, Range(0, 1)] private float _spikeySpawn;
    [SerializeField, Range(0, 1)] private float _slimeSpawn;
    [SerializeField, Range(0, 1)] private float _wormhollSpawn;

    [Header("SpawnChances: difficult enemies")]
    [SerializeField, Range(0, 1)] private float _zombieBossSpawn;
    [SerializeField, Range(0, 1)] private float _flyBossSpawn;
    [SerializeField, Range(0, 1)] private float _SpikeyBossSpawn;


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
        if (Instance == null)
        {
            Instance = this;
        }

        _rightBound = _rightDownCorner.position.x;
        _leftBound = _leftUpCorner.position.x;
        _downBound = _rightDownCorner.position.y;
        _upBound = _leftUpCorner.position.y;
    }

    private void Update()
    {
        if (_aliveEnemiesCount <= 0)
        {
            _waveNumber++;

            if (Random.value > 0.1f)
            {
                StartCoroutine(SpawnEnemyWave(GetRandomSimpleEnemy(), _waveNumber / 2, _spawnRange));
            }
            else
            {
                StartCoroutine(SpawnEnemyWave(GetRandomDifficultEnemy(), 1, _spawnRange + 10));
            }

        }
    }

    public void DecreaseAliveEnemiesCount()
    {
        _aliveEnemiesCount--;
    }

    public void IncreaseAliveEnemiesCount()
    {
        _aliveEnemiesCount++;
    }

    public EnemyType GetRandomSimpleEnemy()
    {
        var randomValue = Random.value;

        if (randomValue < _zombieSpawn) return EnemyType.Zombie;
        if (randomValue < _flySpawn) return EnemyType.Fly;
        if (randomValue < _spikeySpawn) return EnemyType.Spikey;
        if (randomValue < _slimeSpawn) return EnemyType.Slime;
        if (randomValue < _wormhollSpawn) return EnemyType.Wormholl;

        return EnemyType.Default;
    }

    public EnemyType GetRandomDifficultEnemy()
    {
        var randomValue = Random.value;

        if (randomValue < _zombieBossSpawn) return EnemyType.ZombieBoss;  
        if (randomValue < _flyBossSpawn) return EnemyType.FlyBoss;  
        if (randomValue < _SpikeyBossSpawn) return EnemyType.SpikeyBoss;

        return EnemyType.Default;
    }

    private IEnumerator SpawnEnemyWave(EnemyType enemyType, int count, float spawnRadius)
    {
        for (int i = 0; i < count; i++)
        {
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
