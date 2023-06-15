using UnityEngine;

public enum TemporaryUpgrade
{
    nothing, 
    increaseHealth,
    decreaseHealth, 
    increaseSpeed,
    decreaseSpeed,
    increaseDamage,
    decreaseDamage
}

public class FountainController : MonoBehaviour
{
    public static FountainController Instance;

    [SerializeField, Range(0, 1)] private float _successChance;
    [SerializeField] private AudioClip _buySound;

    public TemporaryUpgrade CurrentUpgrade { get { return _currentUpgrade; } }
    private TemporaryUpgrade _currentUpgrade = TemporaryUpgrade.nothing;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void GiveRandomPower()
    {
        SpendCoin();

        if (Random.value > _successChance)
        {
            _currentUpgrade = (TemporaryUpgrade)Random.Range(1, 6);
            Debug.ClearDeveloperConsole();
            print(_currentUpgrade);
            // Всплывает иконка с улучшением
        }
    }

    private void SpendCoin()
    {
        if (PlayerWallet.Instance.IsEnoughMoney(1, 0, 0))
        {
            PlayerWallet.Instance.ReduceResources(1, 0, 0);
            AudioManager.Instance.PlaySound(_buySound);
        }
    }
}
