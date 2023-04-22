using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftTableForm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _soulText;
    [SerializeField] private TextMeshProUGUI _crystalText;

    private int _coinsPrice;
    private int _soulPrice;
    private int _crystalsPrice;

    public void SetValues(CraftableObject obj)
    {
        _image.sprite = obj.IconSprite;
        _moneyText.text = $"{obj.MoneyToBuy}";
        _soulText.text = $"{obj.SoulToBuy}";
        _crystalText.text = $"{obj.CrystalsToBuy}";

        _coinsPrice = obj.MoneyToBuy;
        _soulPrice = obj.SoulToBuy;
        _crystalsPrice = obj.CrystalsToBuy;
    }

    public void Buy()
    {
        if (IsEnoughMoney())
        {
            Destroy(gameObject);
            PlayerWallet.Instance.Coins -= _coinsPrice;
            PlayerWallet.Instance.Soul -= _soulPrice;
            PlayerWallet.Instance.Crystals -= _crystalsPrice;

            SaveSystem.Instance.SaveResourcesData();
            UI_Manager.Instance.UpdateResourcesCount(PlayerWallet.Instance.Coins, PlayerWallet.Instance.Soul, PlayerWallet.Instance.Crystals);
        }
        else
        {
            Debug.Log("NO");
        }
    }

    private bool IsEnoughMoney()
    {
        return (PlayerWallet.Instance.Coins >= _coinsPrice) &&
            (PlayerWallet.Instance.Soul >= _soulPrice) &&
            (PlayerWallet.Instance.Crystals >= _crystalsPrice);
    }
}
