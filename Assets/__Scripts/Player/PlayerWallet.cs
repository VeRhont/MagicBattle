using UnityEngine;

public class PlayerWallet: MonoBehaviour
{
    public static PlayerWallet Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            if (value > 0) _coins = value;
        }
    }
    public int Soul
    {
        get
        {
            return _soul;
        }
        set
        {
            if (value > 0) _soul = value;
        }
    }
    public int Crystals
    {
        get
        {
            return _crystals;
        }
        set
        {
            if (value > 0) _crystals = value;
        }
    }

    private int _coins;
    private int _soul;
    private int _crystals;
}
