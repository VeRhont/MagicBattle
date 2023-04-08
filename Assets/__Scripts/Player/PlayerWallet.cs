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
    private int _coins;
}
