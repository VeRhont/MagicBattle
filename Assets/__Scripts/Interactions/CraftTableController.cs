using UnityEngine;
using System.Collections.Generic;

public class CraftTableController : MonoBehaviour
{
    public static CraftTableController Instance;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _formPrefab;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private List<CraftableObject> _craftableObjects;

    private void Start()
    {
        Instance = this;
        CreateCraftTableUI();
    }

    private void CreateCraftTableUI()
    {
        _craftableObjects.Reverse();
        foreach (var obj in _craftableObjects)
        {
            var name = obj.Name;
            if (PlayerPrefs.GetInt(name, 0) == 1) // if already bought
            {
                TowerController.Instance.AddUpgradableObject(obj.UpgradableObject);
            }
            else
            {
                CreateForm(obj);
            }
        }

        TowerController.Instance.CreateTowerUI();
    }

    private void CreateForm(CraftableObject obj)
    {
        var form = Instantiate(_formPrefab, _contentParent);
        form.GetComponent<CraftTableForm>().SetValues(obj);
    }

    public void ShowShopUI()
    {
        _canvas.SetActive(true);
        UI_Manager.Instance.EnableResourcesCanvas();
    }

    public void HideShopUI()
    {
        _canvas.SetActive(false);
        UI_Manager.Instance.DisableResourcesCanvas();
    }
}
