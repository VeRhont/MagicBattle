using UnityEngine;
using System.Collections.Generic;
using System;

public enum TempUpgrade
{
    increaseHealth,
    decreaseHealth, 
    increaseSpeed,
    decreaseSpeed,
    addCoins,
    addSoul,
    addCrystal,
    nothing
}

[System.Serializable]
public struct TempPowerUp
{
    public float Value;
    public Sprite Image;
};

public class FountainController : MonoBehaviour
{
    public static FountainController Instance;

    [SerializeField] private List<TempPowerUp> _upgradesList;
    public Dictionary<TempUpgrade, TempPowerUp> UpgradeValues = new Dictionary<TempUpgrade, TempPowerUp>();

    [SerializeField, Range(0.9f, 1f)] private float _moneyChance;
    [SerializeField, Range(0f, 1f)] private float _successChance;
    [SerializeField] private AudioClip _buySound;

    public TempUpgrade CurrentUpgrade => _currentUpgrade; 
    private TempUpgrade _currentUpgrade = TempUpgrade.nothing;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        for (int i = 0; i < _upgradesList.Count; i++)
        {
            UpgradeValues[(TempUpgrade)i] = _upgradesList[i];
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void GiveRandomPower()
    {
        SpendCoin();
        var randomValue = UnityEngine.Random.value;

        if (randomValue > _moneyChance)
        {
            var bonus = (TempUpgrade)UnityEngine.Random.Range(4, 6);

            if (bonus == TempUpgrade.addCoins)  
                PlayerWallet.Instance.Coins += (int)UpgradeValues[bonus].Value;           
            else if (bonus == TempUpgrade.addSoul)
                PlayerWallet.Instance.Soul += (int)UpgradeValues[bonus].Value;
            else
                PlayerWallet.Instance.Crystals += (int)UpgradeValues[bonus].Value;

            UI_Manager.Instance.UpdateResourcesCount();
            UI_Manager.Instance.PopUpUpgradeImage(bonus, UpgradeValues[bonus].Image);
        }
        else if (UnityEngine.Random.value > _successChance)
        {
            _currentUpgrade = (TempUpgrade)UnityEngine.Random.Range(0, 3);
            UI_Manager.Instance.PopUpUpgradeImage(_currentUpgrade, UpgradeValues[_currentUpgrade].Image);
        }
    }

    private void SpendCoin()
    {
        if (PlayerWallet.Instance.IsEnoughMoney(1, 0, 0))
        {
            PlayerWallet.Instance.ReduceResources(1, 0, 0);
            AudioManager.Instance.PlaySound(_buySound);
        }
    }
}