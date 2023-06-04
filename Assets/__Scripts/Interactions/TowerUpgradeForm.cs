using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TowerUpgradeForm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _emptySlot;
    [SerializeField] private Sprite _filledSlot;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _soulText;
    [SerializeField] private TextMeshProUGUI _crystalsText;
    [SerializeField] private Transform _slotArea;

    private List<Image> _slots = new List<Image>();

    private UpgradableObject _upgradableObject;
    private string _name;
    private int _currentLevel = 0;
    private int _maxLevel;
    private int _coinsPrice;
    private int _soulPrice;
    private int _crystalsPrice;
    private float _currentValue;
    private float _addForLevel;

    public void SetValues(UpgradableObject obj)
    {
        _upgradableObject = obj;
        _name = _upgradableObject.Name;
        _addForLevel = _upgradableObject.AddForLevel;
        _coinsPrice = Mathf.Max(_upgradableObject.InitialPriceCoins, PlayerPrefs.GetInt($"{_name}Coins"));
        _soulPrice = Mathf.Max(_upgradableObject.InitialPriceSoul, PlayerPrefs.GetInt($"{_name}Soul", _soulPrice));
        _crystalsPrice = Mathf.Max(_upgradableObject.InitialPriceCrystals, PlayerPrefs.GetInt($"{_name}Crystals", _crystalsPrice));     

        _currentLevel = (int)SaveSystem.Instance.LoadUpgradeLevel(_name).x;
        _currentValue = Mathf.Max(_upgradableObject.InitialValue, SaveSystem.Instance.LoadUpgradeLevel(_name).y);

        UpdateUI();
        CreateEmptySlots();
    }

    private void UpdateUI()
    {
        _image.sprite = _upgradableObject.IconSprite;
        _maxLevel = _upgradableObject.MaxLevel;

        _coinsText.SetText($"{_coinsPrice}");
        _soulText.SetText($"{_soulPrice}");
        _crystalsText.SetText($"{_crystalsPrice}");
    }

    private void CreateEmptySlots()
    {
        for (int i = 0; i < _maxLevel; i++)
        {
            var slot = Instantiate(_emptySlot, _slotArea);
            _slots.Add(slot);
        }

        UpdateFilledSlots();
    }

    private void UpdateFilledSlots()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (i < _currentLevel)
            {
                _slots[i].sprite = _filledSlot;
            }
        }
    }

    public void BuyUpgrade()
    {
        if ((_currentLevel < _maxLevel) && (PlayerWallet.Instance.IsEnoughMoney(_coinsPrice, _soulPrice, _crystalsPrice)))
        {
            AudioManager.Instance.PlayBuySound();

            _currentLevel++;
            _currentValue += _addForLevel;

            _coinsPrice = (int)(_coinsPrice * 1.5f);
            _soulPrice = (int)(_soulPrice * 1.5f);
            _crystalsPrice = (int)(_crystalsPrice * 1.5f);

            PlayerWallet.Instance.ReduceResources(_coinsPrice, _soulPrice, _crystalsPrice, showCanvas:false);
            SaveSystem.Instance.SaveUpgradeLevel(_name, _currentLevel, _currentValue);
            SaveSystem.Instance.SaveObjectPrices(_name, _coinsPrice, _soulPrice, _crystalsPrice);

            UpdateFilledSlots();
            UpdateUI();
        }
        else
        {
            AudioManager.Instance.PlayCancelSound();
        }
    }
}
