using UnityEngine;

public struct PlayerData
{
    public float MaxHealth;
    public float Speed;

    public PlayerData(float maxHealth, float speed)
    {
        MaxHealth = maxHealth;
        Speed = speed;
    }
}

public struct WeaponData
{
    public float Damage;
    public float FireRate;

    public WeaponData(float damage, float fireRate)
    {
        Damage = damage;
        FireRate = fireRate;
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
        GameManager.Instance.OnPlayerDie += SaveResourcesData;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDie -= SaveResourcesData;
    }

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

        var damage = PlayerPrefs.GetFloat("damageUpgradeValue", 10);
        var fireRate = PlayerPrefs.GetFloat("fireRateUpgradeValue", 0.5f);

        return new WeaponData(damage, fireRate);
    }

    public void SaveUpgradeLevel(string name, int level, float value)
    {
        PlayerPrefs.SetInt($"{name}Upgrade", level);
        PlayerPrefs.SetFloat($"{name}Value", value);
    }

    public Vector2 LoadUpgradeLevel(string name)
    {
        return new Vector2(PlayerPrefs.GetInt($"{name}Upgrade", 0), PlayerPrefs.GetFloat($"{name}Value", 0));
    }

    public void SaveCraftObject(string name)
    {
        PlayerPrefs.SetInt(name, 1);
    }

    public void SaveObjectPrices(string name, int coinsPrice, int soulPrice, int crystalsPrice)
    {
        PlayerPrefs.SetInt($"{name}Coins", coinsPrice);
        PlayerPrefs.SetInt($"{name}Soul", soulPrice);
        PlayerPrefs.SetInt($"{name}Crystals", crystalsPrice);
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