using UnityEngine;
using System.Collections.Generic;

public class CraftTableController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _formPrefab;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private List<CraftableObject> _craftableObjects;

    private void Awake()
    {
        CreateCraftableTableUI();
    }

    private void CreateCraftableTableUI()
    {
        foreach (var obj in _craftableObjects)
        {
            CreateForm(obj);
        }
    }

    private void CreateForm(CraftableObject obj)
    {
        var form = Instantiate(_formPrefab, _contentParent);
        form.GetComponent<CraftTableForm>().SetValues(obj);
    }

    public void ShowShopUI()
    {
        _canvas.SetActive(true);
    }

    public void HideShopUI()
    {
        _canvas.SetActive(false);
    }
}
