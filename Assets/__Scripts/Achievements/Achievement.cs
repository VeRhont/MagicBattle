using UnityEngine;

[CreateAssetMenu(fileName = "Achievement")]
public class Achievement : ScriptableObject
{
    [SerializeField] private bool _isCollected;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _achievementLockedSprite;
    [SerializeField] private Sprite _achievementUnlockedSprite;

    public bool IsCollected => _isCollected;
    public string Name => _name;
    public string Description => _description;
    public Sprite AchievementLockedSprite => _achievementLockedSprite;
    public Sprite AchievementUnlockedSprite => _achievementUnlockedSprite;
}
