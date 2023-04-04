using UnityEngine;
using System.Collections.Generic;

public class CraftTableController : MonoBehaviour
{
    [SerializeField] private GameObject _formPrefab;
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
        var form = Instantiate(_formPrefab);
        form.GetComponent<CraftTableForm>().SetValues(obj);
    }

    public void ShowShopUI()
    {
        Debug.Log("Show");
    }

    public void HideShopUI()
    {
        Debug.Log("Hide");
    }
}
