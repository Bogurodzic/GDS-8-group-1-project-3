using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : GenericSingletonClass<GameManager>
{
    [SerializeField] private GameObject _playerInventory;
    [SerializeField] private PlayerInventoryUI _playerInventoryUI;
    [SerializeField] private GameObject _inventoryShowcaseGameObject;
    [SerializeField] private AudioController _audioController;
    
    //private LinkedList<Collectible> _collectibles = new LinkedList<Collectible>();

    private bool _inventoryVisible = false;
    public void Update()
    {
        if (!_inventoryVisible && Input.GetKeyDown(KeyCode.I) && Collectibles.collectibles.Count > 0)
        {
            ShowInventory();
        } else if (_inventoryVisible && Input.GetKeyDown(KeyCode.I))
        {
            HideInventory();
        }
    }

    private void ShowInventory()
    {
        PauseGame();
        Inventory.SetInventory(true);
        _inventoryVisible = true;
        _playerInventory.SetActive(true);
        _playerInventory.GetComponent<PlayerInventoryUI>().ShowPlayerInventoryUI();
        _inventoryShowcaseGameObject.SetActive(true);
    }

    private void HideInventory()
    {
        ResumeGame();
        Inventory.SetInventory(false);
        _inventoryVisible = false;
        _playerInventory.SetActive(false);
        _inventoryShowcaseGameObject.SetActive(false);
    }
    

    public void AddCollectible(Collectible collectible)
    {
        Collectibles.collectibles.AddLast(collectible);
        _audioController.collectibleCollected();
        
        Debug.Log("ADDING COLLECTIBLE");
        Debug.Log(Collectibles.collectibles.Count);
    }

    public LinkedList<Collectible> GetCollectibles()
    {
        return Collectibles.collectibles;
    }
    
    void PauseGame ()
    {
        Time.timeScale = 0;
    }

    void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}
