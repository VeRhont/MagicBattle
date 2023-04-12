using UnityEngine;

public enum GameState
{

}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OnPlayerDie()
    {
        SaveSystem.Instance.SaveResourcesData();
        // сохранить статистику
        // загрузить новую сцену
    }
}