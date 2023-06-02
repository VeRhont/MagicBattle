using UnityEngine;
using System.Collections.Generic;

public struct PlayerData
{
    public float _maxHealth;
    public float _speed;

    public PlayerData(float maxHealth, float speed)
    {
        _maxHealth = maxHealth;
        _speed = speed;
    }
}

public struct WeaponData
{
    public float _damage;
    public float _fireRate;

    public WeaponData(float damage, float fireRate)
    {
        _damage = damage;
        _fireRate = fireRate;
    }
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //GameManager.Instance.OnPlayerDie += SavePlayerData;
        GameManager.Instance.OnPlayerDie += SaveResourcesData;
    }

    private void OnDisable()
    {
        //GameManager.Instance.OnPlayerDie -= SavePlayerData;
        GameManager.Instance.OnPlayerDie -= SaveResourcesData;
    }

    //public void SavePlayerData()
    //{        
    //    Debug.Log("Player data saved");

    //    PlayerPrefs.SetFloat("maxHealth", 1);
    //    PlayerPrefs.SetFloat("speed", 1);
    //}

    //public void SaveWeaponData()
    //{
    //    Debug.Log("Weapon data saved");
    //}

    public PlayerData LoadPlayerData()
    {
        Debug.Log("Player data loaded");

        var health = PlayerPrefs.GetFloat("healthUpgradeValue", 100);
        var speed = PlayerPrefs.GetFloat("speedUpgradeValue", 2);

        return new PlayerData(health, speed);
    }

    public WeaponData LoadWeaponData()
    {
        Debug.Log("Weapon data loaded");

        var damage = PlayerPrefs.GetFloat("damageUpgradeValue");
        var fireRate = PlayerPrefs.GetFloat("fireRateUpgradeValue");

        return new WeaponData(damage, fireRate);
    }

    public void SaveUpgradeLevel(string name, int level, float value)
    {
        PlayerPrefs.SetInt($"{name}Upgrade", level);
        PlayerPrefs.SetFloat($"{name}Value", value);
    }

    public Vector2 LoadUpgradeLevel(string name)
    {
        return new Vector2(PlayerPrefs.GetInt($"{name}Upgrade", 0), PlayerPrefs.GetInt($"{name}Value", 0));
    }

    public void SaveCraftObject(string name)
    {
        PlayerPrefs.SetInt(name, 1);
    }

    public void SaveResourcesData()
    {
        PlayerPrefs.SetInt("coinsCount", PlayerWallet.Instance.Coins);
        PlayerPrefs.SetInt("soulCount", PlayerWallet.Instance.Soul);
        PlayerPrefs.SetInt("crystalsCount", PlayerWallet.Instance.Crystals);
    }

    public void LoadResourcesData()
    {
        PlayerWallet.Instance.Coins = PlayerPrefs.GetInt("coinsCount", 0);
        PlayerWallet.Instance.Soul = PlayerPrefs.GetInt("soulCount", 0);
        PlayerWallet.Instance.Crystals = PlayerPrefs.GetInt("crystalsCount", 0);
    }
}