using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        Play();
    }

    public void Play() => ChangeScene.Instance.FadeToScene(SceneType.Tower);
    public void Quit() => Application.Quit();
}
