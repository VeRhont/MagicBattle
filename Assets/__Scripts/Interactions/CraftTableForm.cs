using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftTableForm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _soulText;
    [SerializeField] private TextMeshProUGUI _crystalText;

    public void SetValues(CraftableObject obj)
    {
        _image.sprite = obj.IconSprite;
        _moneyText.text = $"{obj.MoneyToBuy}";
        _soulText.text = $"{obj.SoulToBuy}";
        _crystalText.text = $"{obj.CrystalsToBuy}";
    }
}
