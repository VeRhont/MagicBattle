using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void UpdateHealthBar(int health, int maxHealth)
    {
        _healthBarImage.fillAmount = health / maxHealth;
    }

    public void UpdateScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }

    public void UpdateCoins()
    {

    }

    public void UpdateKills()
    {

    }
}
