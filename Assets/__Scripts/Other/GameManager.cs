using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action OnPlayerDie;

    private void Awake()
    {
        Instance = this;
    }

    public void EndGame()
    {
        SaveSystem.Instance.SaveResourcesData();
        OnPlayerDie?.Invoke();
    }   
}