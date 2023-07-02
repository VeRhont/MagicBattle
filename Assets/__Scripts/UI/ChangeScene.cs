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

    [SerializeField] private bool _isMenu = false;
    private SceneType _sceneToLoad;
    private Animator _animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_isMenu == false)
            GameManager.Instance.OnPlayerDie += ReturnToBase;
    }

    private void OnDisable()
    {
        if (_isMenu == false)
            GameManager.Instance.OnPlayerDie -= ReturnToBase;
    }

    private void ReturnToBase()
    {
        FadeToScene(SceneType.Tower);
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
