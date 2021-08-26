using UnityEngine;

public class InventoryItem
{
    private GameObject _inventoryItemGameObject;
    private ItemUI _inventoryItemUI;
    
    public InventoryItem(GameObject inventoryItemGameObject, ItemUI inventoryItemUI)
    {
        _inventoryItemGameObject = inventoryItemGameObject;
        _inventoryItemUI = inventoryItemUI;
    }

    public GameObject GetGameObject()
    {
        return _inventoryItemGameObject;
    }

    public ItemUI GetItemUI()
    {
        return _inventoryItemUI;
    }
}