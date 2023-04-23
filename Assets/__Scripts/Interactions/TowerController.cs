using UnityEngine;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    public static TowerController Instance;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _formPrefab;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private List<UpgradableObject> _upgrades;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateTowerUI()
    {
        foreach (var obj in _upgrades)
        {
            CreateForm(obj);
        }
    }

    private void CreateForm(UpgradableObject obj)
    {
        var form = Instantiate(_formPrefab, _contentParent);
        form.GetComponent<TowerUpgradeForm>().SetValues(obj);
    }

    public void CreateNewForm(UpgradableObject obj)
    {
        CreateForm(obj);
        AddUpgradableObject(obj);
    }

    public void ShowUpgradesShopUI()
    {
        _canvas.SetActive(true);
    }

    public void HideUpgradesShopUI()
    {
        _canvas.SetActive(false);
    }

    public void AddUpgradableObject(UpgradableObject obj)
    {
        if (obj != null)
        {
            _upgrades.Add(obj);
        }
    }
}
