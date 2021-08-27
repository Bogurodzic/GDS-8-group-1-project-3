using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : GenericSingletonClass<GameManager>
{
    [SerializeField] private GameObject _playerInventory;
    [SerializeField] private PlayerInventoryUI _playerInventoryUI;
    [SerializeField] private GameObject _inventoryShowcaseGameObject;

    private LinkedList<Collectible> _collectibles = new LinkedList<Collectible>();

    private bool _inventoryVisible = false;
    public void Update()
    {
        if (!_inventoryVisible && Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        } else if (_inventoryVisible && Input.GetKeyDown(KeyCode.I))
        {
            HideInventory();
        }
    }

    private void ShowInventory()
    {
        Inventory.SetInventory(true);
        _inventoryVisible = true;
        _playerInventory.SetActive(true);
        _playerInventory.GetComponent<PlayerInventoryUI>().ShowPlayerInventoryUI();
        _inventoryShowcaseGameObject.SetActive(true);
    }

    private void HideInventory()
    {
        Inventory.SetInventory(false);
        _inventoryVisible = false;
        _playerInventory.SetActive(false);
        _inventoryShowcaseGameObject.SetActive(false);
    }
    

    public void AddCollectible(Collectible collectible)
    {
        _collectibles.AddLast(collectible);
        
        Debug.Log("ADDING COLLECTIBLE");
        Debug.Log(_collectibles.Count);
    }

    public LinkedList<Collectible> GetCollectibles()
    {
        return _collectibles;
    }
}
