using UnityEngine;

[CreateAssetMenu(fileName = "UpgradableObject")]
public class UpgradableObject : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private int _maxLevel;
    [SerializeField] private int _initialPriceCoins;
    [SerializeField] private int _initialPriceSoul;
    [SerializeField] private int _initialPriceCrystals;

    public string Name => _name;
    public Sprite IconSprite => _iconSprite;
    public int MaxLevel => _maxLevel;
    public int InitialPriceCoins => _initialPriceCoins; 
    public int InitialPriceSoul => _initialPriceSoul; 
    public int InitialPriceCrystals => _initialPriceCrystals; 
}
