using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Menu, 
    EndlessMode,
    Tower
}

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene Instance;

    private SceneType _sceneToLoad;
    private Animator _animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _animator = GetComponent<Animator>();
    }

    public void FadeToScene(SceneType sceneType)
    {
        _sceneToLoad = sceneType;
        _animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene((int)_sceneToLoad);
    }
}
