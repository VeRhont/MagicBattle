using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private float _offset = 1f;
    [SerializeField] private float _xBound;
    [SerializeField] private float _yBound;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemiesPrefabs;
    [SerializeField] private GameObject[] _powerUpsPrefabs;

    [SerializeField] private float _minTimeBetweenEnemySpawn = 0.3f;
    [SerializeField] private float _maxTimeBetweenPowerUpSpawn = 10f;
    [SerializeField] private float _timeBetweenEnemySpawn = 3;
    [SerializeField] private float _timeBetweenPowerUpSpawn = 5;

    private void Start()
    {
        Invoke("SpawnEnemy", _timeBetweenEnemySpawn);
        Invoke("SpawnPowerUp", _timeBetweenPowerUpSpawn);
    }

    private void Update()
    {
        if (_timeBetweenEnemySpawn > _minTimeBetweenEnemySpawn)
        {
            _timeBetweenEnemySpawn -= (Time.deltaTime / 150);
        }
        if (_timeBetweenPowerUpSpawn < _maxTimeBetweenPowerUpSpawn)
        {
            _timeBetweenPowerUpSpawn += (Time.deltaTime / 150);
        }        
    }

    private void SpawnEnemy()
    {
        var randomIndex = Random.Range(0, _spawnPoints.Length);
        var spawnPoint = _spawnPoints[randomIndex].position;

        randomIndex = Random.Range(0, _enemiesPrefabs.Length);

        var enemy = Instantiate<GameObject>(_enemiesPrefabs[randomIndex], spawnPoint, Quaternion.identity);

        Invoke("SpawnEnemy", _timeBetweenEnemySpawn);
    }

    private void SpawnPowerUp()
    {
        var randomIndex = Random.Range(0, _powerUpsPrefabs.Length);
        var randomPosition = GetRandomPosition();

        var powerUp = Instantiate(_powerUpsPrefabs[randomIndex], randomPosition, Quaternion.identity);

        Invoke("SpawnPowerUp", _timeBetweenPowerUpSpawn);
    }

    private Vector2 GetRandomPosition()
    {
        float x = _offset * Mathf.RoundToInt(Random.Range(-1 * _xBound, _xBound));
        float y = _offset * Mathf.RoundToInt(Random.Range(-1 * _yBound, _yBound));

        return new Vector2((int)x, (int)y);
    }
}