using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryItem;
    [SerializeField] private PlayerInventory _playerInventory;

    private LinkedList<InventoryItem> _inventoryItems = new LinkedList<InventoryItem>();
    private int _currentIndex = 0;

    public void Update()
    {
        if (Inventory.IsInventoryOpened)
        {
            HandleInventoryControls();
        }
    }

    public void HandleInventoryControls()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NextItem();
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PreviousItem();
        }
    }

    private void NextItem()
    {
        HideAllItems();
        if (_currentIndex < _inventoryItems.Count - 1)
        {
            ShowItem(_currentIndex + 1);
        }
        else
        {
            ShowItem(0);
        }
    }

    private void PreviousItem()
    {
        HideAllItems();
        if (_currentIndex > 0)
        {
            ShowItem(_currentIndex - 1);
        }
        else
        {
            ShowItem(_inventoryItems.Count - 1);
        }
    }

    public void ShowPlayerInventoryUI()
    {
        ClearInventory();
        ClearNewInventory();
        ShowItem(0);
    }

    private void ClearInventory()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        _inventoryItems.Clear();
        _currentIndex = 0;
    }

    private void ClearNewInventory()
    {
        LinkedList<Collectible> collectibles = _playerInventory.GetCollectibles();
        Debug.Log(collectibles.Count);
        int index = 0;
        
        foreach (var collectible in collectibles)
        {
            Vector3 itemPosition = new Vector3(transform.position.x, transform.position.y - 50 * index, transform.position.z);
            GameObject inventoryItemGameObject = Instantiate(_inventoryItem, itemPosition, transform.rotation, transform);
            ItemUI playerInventoryItemUI = inventoryItemGameObject.GetComponent<ItemUI>();
            playerInventoryItemUI.AddCollectible(collectible);
            _inventoryItems.AddLast(new InventoryItem(inventoryItemGameObject, playerInventoryItemUI));
            index++;
        }
    }

    public void ShowItem(int index)
    {
        InventoryItem inventoryItem = _inventoryItems.ElementAt(index);
        inventoryItem.GetItemUI().SetActive(true);
        _currentIndex = index;
    }

    public void HideItem(int index)
    {
        InventoryItem inventoryItem = _inventoryItems.ElementAt(index);
        inventoryItem.GetItemUI().SetActive(false);
    }

    private void HideAllItems()
    {
        foreach (var inventoryItem in _inventoryItems)
        {
            inventoryItem.GetItemUI().SetActive(false);
        }
    }
 }
