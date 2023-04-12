using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SavePlayerData()
    {
        Debug.Log("Player data saved");
    }

    public void LoadPlayerData()
    {
        Debug.Log("Player data loaded");
    }

    public void SaveWeaponData()
    {
        Debug.Log("Weapon data saved");
    }

    public void LoadWeaponData()
    {
        Debug.Log("Weapon data loaded");
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