using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftTableForm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _soulText;
    [SerializeField] private TextMeshProUGUI _crystalText;

    private CraftableObject _craftableObject;
    private int _coinsPrice;
    private int _soulPrice;
    private int _crystalsPrice;
    private string _name;

    public void SetValues(CraftableObject obj)
    {
        _image.sprite = obj.IconSprite;
        _moneyText.text = $"{obj.MoneyToBuy}";
        _soulText.text = $"{obj.SoulToBuy}";
        _crystalText.text = $"{obj.CrystalsToBuy}";

        _craftableObject = obj;
        _coinsPrice = obj.MoneyToBuy;
        _soulPrice = obj.SoulToBuy;
        _crystalsPrice = obj.CrystalsToBuy;
        _name = obj.Name;
    }
    
    public void Buy()
    {
        if (PlayerWallet.Instance.IsEnoughMoney(_coinsPrice, _soulPrice, _crystalsPrice))
        {
            AudioManager.Instance.PlayBuySound();

            PlayerWallet.Instance.ReduceResources(_coinsPrice, _soulPrice, _crystalsPrice);

            SaveSystem.Instance.SaveCraftObject(_name);
            TowerController.Instance.CreateNewForm(_craftableObject.UpgradableObject);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("NO");
        }
    }
}
