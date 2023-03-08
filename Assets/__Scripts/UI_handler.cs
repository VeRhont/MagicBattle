using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_handler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("EndlessMode");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
