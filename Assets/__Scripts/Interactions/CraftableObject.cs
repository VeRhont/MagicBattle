using UnityEngine;

[CreateAssetMenu(fileName = "Craftable Objects")]
public class CraftableObject : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private int _moneyToBuy;
    [SerializeField] private int _soulToBuy;
    [SerializeField] private int _crystalsToBuy;
    [SerializeField] private UpgradableObject _upgradableObject;

    public string Name => _name;
    public Sprite IconSprite => _iconSprite;
    public int MoneyToBuy => _moneyToBuy;
    public int SoulToBuy => _soulToBuy;
    public int CrystalsToBuy => _crystalsToBuy;
    public UpgradableObject UpgradableObject => _upgradableObject;
}
