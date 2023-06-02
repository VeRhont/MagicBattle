using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Action OnPlayerDie;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerWallet.Instance.Coins += 1000;
            PlayerWallet.Instance.Soul += 100;
            PlayerWallet.Instance.Crystals += 10;

            SaveSystem.Instance.SaveResourcesData();
            UI_Manager.Instance.UpdateResourcesCount();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Delete");
            PlayerPrefs.DeleteAll();

            PlayerWallet.Instance.Coins = 0;
            PlayerWallet.Instance.Soul = 0;
            PlayerWallet.Instance.Crystals = 0;

            SaveSystem.Instance.SaveResourcesData();
            UI_Manager.Instance.UpdateResourcesCount();

            Debug.Log(PlayerWallet.Instance.Coins);
            Debug.Log(PlayerWallet.Instance.Soul);
            Debug.Log(PlayerWallet.Instance.Crystals);
        }
    }

    public void EndGame()
    {
        StartCoroutine(UI_Manager.Instance.ChangeKillsToCoins());

        StartCoroutine(FinishGame());
    } 
    
    private IEnumerator FinishGame()
    {       
        yield return new WaitForSeconds(3f);
        OnPlayerDie?.Invoke();
    }
}