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
    private float _value;

    public void SetValues(UpgradableObject obj)
    {
        _upgradableObject = obj;
        _name = _upgradableObject.Name;
        _coinsPrice = _upgradableObject.InitialPriceCoins;
        _soulPrice = _upgradableObject.InitialPriceSoul;
        _crystalsPrice = _upgradableObject.InitialPriceCrystals;

        _currentLevel = (int)SaveSystem.Instance.LoadUpgradeLevel(_name).x;
        _value = Mathf.Max(_upgradableObject.InitialValue, SaveSystem.Instance.LoadUpgradeLevel(_name).y);

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
            _currentLevel++;
            _value = (int)(_value * 1.5f);

            PlayerWallet.Instance.ReduceResources(_coinsPrice, _soulPrice, _crystalsPrice);
            SaveSystem.Instance.SaveUpgradeLevel(_name, _currentLevel, _value);          

            _coinsPrice = (int)(_coinsPrice * 1.5f);
            _soulPrice = (int)(_soulPrice * 1.5f);
            _crystalsPrice = (int)(_crystalsPrice * 1.5f);

            UpdateUI();
            UpdateFilledSlots();
        }
    }
}
