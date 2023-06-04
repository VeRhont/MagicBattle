using UnityEngine;
using System;

public class PlayerWallet: MonoBehaviour
{
    public static PlayerWallet Instance;

    private void Awake()
    {
        Instance = this;
        LoadResourcesCount();
    }

    private void LoadResourcesCount()
    {
        SaveSystem.Instance.LoadResourcesData();
        UI_Manager.Instance.UpdateResourcesCount();
    }

    public bool IsEnoughMoney(int coinsPrice, int soulPrice, int crystalsPrice)
    {
        return (Coins >= coinsPrice) && (Soul >= soulPrice) && (Crystals >= crystalsPrice);
    }

    public void ReduceResources(int coinsPrice, int soulPrice, int crystalsPrice, bool showCanvas=true)
    {
        Coins -= coinsPrice;
        Soul -= soulPrice;
        Crystals -= crystalsPrice;

        SaveSystem.Instance.SaveResourcesData();
        UI_Manager.Instance.UpdateResourcesCount(showCanvas);
    }

    public int Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            if (value >= 0) _coins = value;
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
            if (value >= 0) _soul = value;
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
            if (value >= 0) _crystals = value;
        }
    }

    private int _coins;
    private int _soul;
    private int _crystals;
}
