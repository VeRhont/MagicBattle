using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchivementForm : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    public void SetValues(Achievement achivement)
    {
        _name.SetText(achivement.Name);
        _description.SetText(achivement.Description);

        if (achivement.IsCollected)
        {
            _image.sprite = achivement.AchievementUnlockedSprite;
        }
        else
        {
            _image.sprite = achivement.AchievementLockedSprite;
        }
    }
}
