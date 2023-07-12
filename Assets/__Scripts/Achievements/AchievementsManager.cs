using UnityEngine;
using System.Collections.Generic;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance;

    [SerializeField] private List<Achievement> _achievements;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _achievementForm;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        CreateAchivements();
    }

    private void CreateAchivements()
    {
        foreach (var achievement in _achievements)
        {
            CreateForm(achievement);
        }
    }

    private void CreateForm(Achievement achievement)
    {
        var form = Instantiate(_achievementForm, _content).GetComponent<AchivementForm>();
        form.SetValues(achievement);
    }
}
