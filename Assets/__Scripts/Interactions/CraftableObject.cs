using UnityEngine;

[CreateAssetMenu(fileName = "Craftable Objects")]
public class CraftableObject : ScriptableObject
{
    [SerializeField] private Sprite _iconSprite;   
    [SerializeField] private int _moneyToBuy;
    [SerializeField] private int _soulToBuy;
    [SerializeField] private int _crystalsToBuy;

    public Sprite IconSprite { get { return _iconSprite; } }
    public int MoneyToBuy { get { return _moneyToBuy; } }
    public int SoulToBuy { get { return _soulToBuy; } }
    public int CrystalsToBuy { get { return _crystalsToBuy; } }
}
