using System.Collections.Generic;
using UnityEngine;

public class SpawnEnviroment : MonoBehaviour
{
    [Header("Enviroment")]
    [SerializeField] private float _spawnChance;
    [SerializeField] private float _enviromentCount;
    [SerializeField] private Transform _enviromentParent;
    [SerializeField] private GameObject[] _enviromentPrefabs;

    private List<Vector2> _map;

    private void Start()
    {       
        GenerateMap();
        SpawnRandomEnviroment();
    }

    private Vector2 GetRandomPosition()
    {
        return PositionGenerator.Instance.GetRandomPosition();
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