using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    public int KillsCount => int.Parse(_killsCount.text); 

    [Header("PlayerStats")]
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TextMeshProUGUI _killsCount;

    [Header("Resources")]
    [SerializeField] private GameObject _resourcesCanvas;
    [SerializeField] private TextMeshProUGUI _coinsCountText;
    [SerializeField] private TextMeshProUGUI _soulCountText;
    [SerializeField] private TextMeshProUGUI _crystalsCountText;
    [SerializeField] private float _showCanvasTime = 5;

    [Header("Other UI")]
    [SerializeField] private Image _popUpImage;
    
    private void Awake()
    {
        Instance = this;
    }

    public void UpdatePlayerHealth(float health, float maxHealth)
    {
        _healthBarImage.fillAmount = health / maxHealth;
    }

    public void IncreaseKillsCount()
    {
        int currentCount = KillsCount;
        _killsCount.SetText($"{currentCount + 1}");
    }

    public void DecreaseKillsCount()
    {
        int currentCount = KillsCount;
        _killsCount.SetText($"{currentCount - 1}");
    }

    public IEnumerator ChangeKillsToResourses()
    {
        EnableResourcesCanvas();
        float waitTime = 3f / KillsCount;

        for (int i = KillsCount; i > 0; i--)
        {
            DecreaseKillsCount();
            PlayerWallet.Instance.Coins += 2;
            _coinsCountText.SetText($"{PlayerWallet.Instance.Coins}");

            if (i % 10 == 0)
            {
                PlayerWallet.Instance.Soul += 1;
                _soulCountText.SetText($"{PlayerWallet.Instance.Soul}");
            }

            if (i % 100 == 0)
            {
                PlayerWallet.Instance.Crystals += 1;
                _crystalsCountText.SetText($"{PlayerWallet.Instance.Crystals}");
            }

            yield return new WaitForSeconds(waitTime);
        }

        SaveSystem.Instance.SaveResourcesData();
    }

    public void UpdateResourcesCount(bool showCanvas=true)
    {
        Debug.Log(showCanvas);
        if (showCanvas)
        {
            StartCoroutine(ShowResourcesCanvas());
        }

        var coins = PlayerWallet.Instance.Coins;
        var soul = PlayerWallet.Instance.Soul;
        var crystals = PlayerWallet.Instance.Crystals;

        _coinsCountText.SetText($"{coins}");
        _soulCountText.SetText($"{soul}");
        _crystalsCountText.SetText($"{crystals}");
    }

    public void PopUpUpgradeImage(TempUpgrade currentUpgrade, Sprite newSprite)
    {
        Debug.ClearDeveloperConsole();
        Debug.Log(currentUpgrade);
        Debug.Log((int)currentUpgrade);

        _popUpImage.sprite = newSprite;
        StartCoroutine(FadeImage(_popUpImage));
    }

    private IEnumerator FadeImage(Image image)
    {
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        image.gameObject.SetActive(false);
    }

    public void EnableResourcesCanvas()
    {
        _resourcesCanvas.SetActive(true);
    }

    public void DisableResourcesCanvas()
    {
        _resourcesCanvas.SetActive(false);
    }

    private IEnumerator ShowResourcesCanvas()
    {
        _resourcesCanvas.SetActive(true);
        yield return new WaitForSeconds(_showCanvasTime);
        _resourcesCanvas.SetActive(false);
    }
}
