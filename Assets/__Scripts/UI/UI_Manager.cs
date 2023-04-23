using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    [Header("PlayerStats")]
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TextMeshProUGUI _killsCount;

    [Header("Resources")]
    [SerializeField] private GameObject _resourcesCanvas;
    [SerializeField] private TextMeshProUGUI _coinsCountText;
    [SerializeField] private TextMeshProUGUI _soulCountText;
    [SerializeField] private TextMeshProUGUI _crystalsCountText;
    [SerializeField] private float _showCanvasTime = 5;
    
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
        int currentCount = int.Parse(_killsCount.text);
        _killsCount.SetText($"{currentCount + 1}");
    }

    public void UpdateResourcesCount()
    {
        StartCoroutine(ShowResourcesCanvas());

        var coins = PlayerWallet.Instance.Coins;
        var soul = PlayerWallet.Instance.Soul;
        var crystals = PlayerWallet.Instance.Crystals;

        _coinsCountText.SetText($"{coins}");
        _soulCountText.SetText($"{soul}");
        _crystalsCountText.SetText($"{crystals}");
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
