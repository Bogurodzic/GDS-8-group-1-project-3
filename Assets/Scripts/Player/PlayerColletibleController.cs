using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColletibleController : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;

    [HideInInspector] public int collectibleCount = 0;
    [HideInInspector] public int readableCount = 0;
    
    private bool _coinIsCollecting = false;

    public void Start()
    {
        //LoadComponents();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Coin"))
        {
            if (!_coinIsCollecting)
            {
                _coinIsCollecting = true;
                Collectible collectible = other.gameObject.GetComponent<Collectible>(); 
                _playerInventory.AddCollectible(collectible);
                Destroy(other.gameObject);
                collectibleCount++;
                _coinIsCollecting = false;
            }

            

        }
    }

    //private void LoadComponents()
    //{
    //    _playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
    //}
}
