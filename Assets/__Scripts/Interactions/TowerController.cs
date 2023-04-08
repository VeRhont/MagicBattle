using UnityEngine;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _formPrefab;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private List<UpgradableObject> _upgrades;

    private void Awake()
    {
        CreateUpgradeUI();
    }

    private void CreateUpgradeUI()
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

    public void ShowUpgradesShopUI()
    {
        _canvas.SetActive(true);
    }

    public void HideUpgradesShopUI()
    {
        _canvas.SetActive(false);
    }
}
