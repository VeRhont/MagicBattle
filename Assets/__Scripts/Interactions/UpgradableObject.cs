using UnityEngine;

[CreateAssetMenu(fileName = "UpgradableObject")]
public class UpgradableObject : ScriptableObject
{
    [SerializeField] private Sprite _iconSprite;
    public Sprite IconSprite { get { return _iconSprite; } }

    [SerializeField] private int _maxLevel;
    public int MaxLevel { get { return _maxLevel; } }

    [SerializeField] private int _initialPrice;
    public int InitialPrice { get { return _initialPrice; } }
}
