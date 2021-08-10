using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColletibleController : MonoBehaviour
{
    private PlayerInventory _playerInventory;
    public void Start()
    {
        LoadComponents();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER ENTER");
        if (other.CompareTag("Coin"))
        {
            Debug.Log("TRIGGER ENTER COIN");

            Collectible collectible = other.gameObject.GetComponent<Collectible>();
            _playerInventory.AddCollectible(collectible);
            Destroy(other.gameObject);
        }
    }

    private void LoadComponents()
    {
        _playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
    }
}
