using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _timeBetweenSpawn;

    private int _waveNumber = 1;
    private int _aliveEnemiesCount = 0;
    public int AliveEnemiesCount
    {
        get { return _aliveEnemiesCount; }
        set { _aliveEnemiesCount = value; }
    }

    private List<EnemyType> _possibleSpawns;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _possibleSpawns = new List<EnemyType>();

        _possibleSpawns.Add(EnemyType.Zombie);
        _possibleSpawns.Add(EnemyType.Zombie);
        _possibleSpawns.Add(EnemyType.Fly);
        _possibleSpawns.Add(EnemyType.Spikey);
    }

    private void Update()
    {
        if (_aliveEnemiesCount <= 0)
        {
            _waveNumber++;

            var enemiesToSpawn = _waveNumber / 2 + 1;
            StartCoroutine(SpawnEnemyWave(enemiesToSpawn, _spawnRange));

            if (_waveNumber <= 7)
            {
                // 1..7 Only simple enemies (zombie, spikey, fly)             
            }
            else if (_waveNumber <= 15)
            {
                // 8..15 Slime and ZombieBoss
                _possibleSpawns.Add(EnemyType.Zombie);
                _possibleSpawns.Add(EnemyType.Zombie);
                _possibleSpawns.Add(EnemyType.Fly);
                _possibleSpawns.Add(EnemyType.Fly);
                _possibleSpawns.Add(EnemyType.Spikey);
                _possibleSpawns.Add(EnemyType.Spikey);
                _possibleSpawns.Add(EnemyType.Slime);
                _possibleSpawns.Add(EnemyType.ZombieBoss);
            }
            else if (_waveNumber <= 25)
            {
                // 16..25 Wormholl and FlyBoss
                _possibleSpawns.Add(EnemyType.Zombie);
                _possibleSpawns.Add(EnemyType.Fly);
                _possibleSpawns.Add(EnemyType.Spikey);
                _possibleSpawns.Add(EnemyType.Slime);
                _possibleSpawns.Add(EnemyType.Wormholl);
                _possibleSpawns.Add(EnemyType.FlyBoss);
            }
            else if (_waveNumber <= 40)
            {
                // 26..40 SpikeyBoss and plant
                _possibleSpawns.Add(EnemyType.Zombie);
                _possibleSpawns.Add(EnemyType.Fly);
                _possibleSpawns.Add(EnemyType.Spikey);
                _possibleSpawns.Add(EnemyType.SpikeyBoss);
            }
            else if (_waveNumber == 50)
            {
                // First BlackKnight fight
                Debug.Log("BOSS");
            }
        }
    }

    private IEnumerator SpawnEnemyWave(int count, float spawnRadius)
    {
        for (int i = 0; i < count; i++)
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

    private bool IsInsideBounds(Vector2 position)
    {
        return PositionGenerator.Instance.IsInsideBounds(position);
    }
}
