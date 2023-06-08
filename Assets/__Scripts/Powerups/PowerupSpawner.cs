using UnityEngine;
using System.Collections.Generic;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private Transform _powerupsParent;

    [Header("Chests")]
    [SerializeField] private List<ChestObject> _chestsTypes;
    [SerializeField] private GameObject _chestPrefab;
    [SerializeField] private float _timeBetweenChestSpawn;

    [Header("Heal")]
    [SerializeField] private GameObject _healPotionPrefab;
    [SerializeField] private float _timeBetweenHealPotionSpawn;

    [Header("Laser")]
    [SerializeField] private GameObject _laserPowerupPrefab;
    [SerializeField] private float _timeBetweenLaserPowerupSpawn;

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

    private void Start()
    {
        Invoke("SpawnChest", _timeBetweenChestSpawn);

        if (PlayerPrefs.GetInt("healPotion", 0) == 1) // if health potion is bought
            Invoke("SpawnHealPotion", _timeBetweenHealPotionSpawn);

        if (PlayerPrefs.GetInt("laserPowerup", 0) == 1) // if laser is bought
            Invoke("SpawnLaserPowerup", _timeBetweenLaserPowerupSpawn);
    }

    private void SpawnChest()
    {
        var position = GetRandomPosition();
        var randomValue = Random.value;

        foreach (var chest in _chestsTypes)
        {
            if (randomValue <= chest.ValueToSpawn)
            {
                var c = Instantiate(_chestPrefab, position, Quaternion.identity, _powerupsParent);
                c.GetComponent<Chest>().SetChestValues(chest);

                break;
            }
        }

        Invoke("SpawnChest", _timeBetweenChestSpawn);
    }

    private void SpawnHealPotion()
    {
        var position = GetRandomPosition();
        Instantiate(_healPotionPrefab, position, _healPotionPrefab.transform.rotation, _powerupsParent);

        Invoke("SpawnHealPotion", _timeBetweenHealPotionSpawn);
    }

    private void SpawnLaserPowerup()
    {
        var position = GetRandomPosition();
        Instantiate(_laserPowerupPrefab, position, _laserPowerupPrefab.transform.rotation, _powerupsParent);

        Invoke("SpawnLaserPowerup", _timeBetweenLaserPowerupSpawn);
    }

    private Vector2 GetRandomPosition()
    {
        int x =  Mathf.RoundToInt(Random.Range(_leftBound, _rightBound));
        int y =  Mathf.RoundToInt(Random.Range(_downBound, _upBound));

        return new Vector2(x, y);
    }
}
