using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TowerUpgradeForm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _emptySlot;
    [SerializeField] private Sprite _filledSlot;
    [SerializeField] private Transform _slotArea;

    private List<Image> _slots = new List<Image>();

    private int _initialPrice;
    private int _currentLevel = 0;
    private int _maxLevel;

    public void SetValues(UpgradableObject obj)
    {
        _image.sprite = obj.IconSprite;
        _maxLevel = obj.MaxLevel;
        _initialPrice = obj.InitialPrice;

        CreateEmptySlots();
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
        if (_currentLevel < _maxLevel)
        {
            Debug.Log(_initialPrice);
            _currentLevel++;
            UpdateFilledSlots();
        }
    }
}
