using System.Collections.Generic;
using UnityEngine;

public class SpawnEnviroment : MonoBehaviour
{
    [Header("Enviroment")]
    [SerializeField] private float _spawnChance;
    [SerializeField] private float _enviromentCount;
    [SerializeField] private Transform _enviromentParent;
    [SerializeField] private GameObject[] _enviromentPrefabs;

    [Header("Coordinates")]
    [SerializeField] private Transform _leftUpCorner;
    [SerializeField] private Transform _rightDownCorner;

    private float _rightBound;
    private float _leftBound;
    private float _upBound;
    private float _downBound;

    private List<Vector2> _map;

    private void Awake()
    {
        _rightBound = _rightDownCorner.position.x;
        _leftBound = _leftUpCorner.position.x;
        _downBound = _rightDownCorner.position.y;
        _upBound = _leftUpCorner.position.y;
    }

    private void Start()
    {
        GenerateMap();
        SpawnRandomEnviroment();
    }

    private Vector2 GetRandomPosition()
    {
        int x = Mathf.RoundToInt(Random.Range(_leftBound, _rightBound));
        int y = Mathf.RoundToInt(Random.Range(_downBound, _upBound));

        return new Vector2(x, y);
    }

    private void GenerateMap()
    {
        _map = new List<Vector2>();

        for (int i = 0; i <= _enviromentCount; i++)
        {
            var position = GetRandomPosition();

            while (_map.Contains(position))
            {
                position = GetRandomPosition();
            }

            _map.Add(position);
        }
    }

    private void SpawnRandomEnviroment()
    {
        foreach (var spawnPosition in _map)
        {
            if (Random.value > _spawnChance) continue;

            var randomIndex = Random.Range(0, _enviromentPrefabs.Length);
            var objectToSpawn = _enviromentPrefabs[randomIndex];

            Instantiate(objectToSpawn, spawnPosition, objectToSpawn.transform.rotation, _enviromentParent);
        }
    }
}