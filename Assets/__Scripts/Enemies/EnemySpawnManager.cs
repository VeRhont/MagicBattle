using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("Spawning")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _timeBetweenSpawn;

    [Header("Balance")]
    [SerializeField] private int _firstWave;
    [SerializeField] private int _secondWave;
    [SerializeField] private int _thirdWave;
    [SerializeField] private int _bossWave;
    [SerializeField] private int _availablePerWave;
    [SerializeField] private int _aliveEnemiesCount = 0;
    private int _waveNumber = 0;

    public int AliveEnemiesCount
    {
        get
        {
            return _aliveEnemiesCount;
        }
        set
        {
            _aliveEnemiesCount = value;
        }
    }
    public int AvailablePerWave
    {
        get
        {
            return _availablePerWave;
        }
        set
        {
            _availablePerWave = value;
        }
    }

    private List<EnemyType> _possibleSpawns;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        _possibleSpawns = new List<EnemyType>();
        AddEnemiesToList(EnemyType.Zombie, EnemyType.Zombie, EnemyType.Fly, EnemyType.Spikey);
 
        EnemyFactory.Instance.EnemySpawned += OnEnemySpawned;
    }

    private void OnDestroy()
    {
        EnemyFactory.Instance.EnemySpawned -= OnEnemySpawned;
    }

    private void OnEnemySpawned(int spawnPrice)
    {
        AliveEnemiesCount += 1;
        AvailablePerWave -= spawnPrice;
    }

    private void Update()
    {
        if (AliveEnemiesCount <= 0)
        {
            _waveNumber++;
            AvailablePerWave = _waveNumber + 1;

            StartCoroutine(SpawnEnemyWave(_spawnRange));

            if (_waveNumber == _firstWave)
            {
                AddEnemiesToList(EnemyType.Zombie, EnemyType.Zombie, EnemyType.Fly, 
                           EnemyType.Fly, EnemyType.Spikey, EnemyType.Spikey,
                           EnemyType.Slime, EnemyType.FlyBoss);
            }
            else if (_waveNumber == _secondWave)
            {
                AddEnemiesToList(EnemyType.Zombie, EnemyType.Fly, EnemyType.Spikey,
                           EnemyType.Slime, EnemyType.Wormholl, EnemyType.ZombieBoss, EnemyType.Plant);
            }
            else if (_waveNumber == _thirdWave)
            {
                AddEnemiesToList(EnemyType.Zombie, EnemyType.Fly, EnemyType.Spikey,
                           EnemyType.SpikeyBoss);
            }
            else if (_waveNumber == _bossWave)
            {
                Debug.Log("BOSS");
            }
        }
    }

    private void AddEnemiesToList(params EnemyType[] enemiesToAdd)
    {
        foreach (var enemy in enemiesToAdd)
            _possibleSpawns.Add(enemy);
    }

    private IEnumerator SpawnEnemyWave(float spawnRadius)
    {
        while (AvailablePerWave > 0)
        {
            var randomIndex = Random.Range(0, _possibleSpawns.Count);
            var spawnPosition = GetPosition(spawnRadius);

            EnemyFactory.Instance.CreateEnemy(_possibleSpawns[randomIndex], spawnPosition);

            yield return new WaitForSeconds(Random.Range(0, _timeBetweenSpawn));
        }
    }

    private Vector2 GetPosition(float spawnRadius)
    {
        return PositionGenerator.Instance.GetPositionAroundPlayer(spawnRadius, _playerTransform);
    }
}