using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _mainUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;

        _pauseMenuUI.SetActive(false);
        _mainUI.SetActive(true);
    }

    public void Pause()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;

        _pauseMenuUI.SetActive(true);
        _mainUI.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Resume();
        Time.timeScale = 1;
        ChangeScene.Instance.FadeToScene(SceneType.Menu);
    }

    public void ReturnToBase()
    {
        Resume();
        Time.timeScale = 1;
        ChangeScene.Instance.FadeToScene(SceneType.Tower);
    }
}
