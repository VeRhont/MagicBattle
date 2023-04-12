using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    [SerializeField] private Image _healthBarImage;
    [SerializeField] private GameObject _resourcesCanvas;
    [SerializeField] private TextMeshProUGUI _coinsCountText;
    [SerializeField] private TextMeshProUGUI _soulCountText;
    [SerializeField] private TextMeshProUGUI _crystalsCountText;
    

    private void Awake()
    {
        Instance = this;
    }

    public void UpdatePlayerHealth(float health, float maxHealth)
    {
        _healthBarImage.fillAmount = health / maxHealth;
    }

    public void UpdateResourcesCount(int coins, int soul, int crystals)
    {
        StartCoroutine(ShowResourcesCanvas());

        _coinsCountText.SetText($"{coins}");
        _soulCountText.SetText($"{soul}");
        _crystalsCountText.SetText($"{crystals}");
    }

    private IEnumerator ShowResourcesCanvas()
    {
        _resourcesCanvas.SetActive(true);
        yield return new WaitForSeconds(5);
        _resourcesCanvas.SetActive(false);
    }
}