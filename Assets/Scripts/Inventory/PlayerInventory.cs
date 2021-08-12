using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : GenericSingletonClass<GameManager>
{
    [SerializeField] private GameObject _playerInventory;
    [SerializeField] private PlayerInventoryUI _playerInventoryUI;
    
    private LinkedList<Collectible> _collectibles = new LinkedList<Collectible>();

    private bool _inventoryVisible = false;
    public void Update()
    {
        if (!_inventoryVisible && Input.GetKeyDown(KeyCode.I))
        {
            _inventoryVisible = true;
            _playerInventory.SetActive(true);
            _playerInventory.GetComponent<PlayerInventoryUI>().ShowPlayerInventoryUI();
        } else if (_inventoryVisible && Input.GetKeyDown(KeyCode.I))
        {
            _inventoryVisible = false;
            _playerInventory.SetActive(false);
        }
    }

    public void AddCollectible(Collectible collectible)
    {
        _collectibles.AddLast(collectible);
    }

    public LinkedList<Collectible> GetCollectibles()
    {
        return _collectibles;
    }
}
