using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryItem; 
    
    public void ShowPlayerInventoryUI()
    {
        LinkedList<Collectible> collectibles =
            GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>().GetCollectibles();

        Debug.Log(collectibles.Count);
        int index = 0;
        foreach (var collectible in collectibles)
        {
            Vector3 itemPosition = new Vector3(transform.position.x, transform.position.y - 50 * index, transform.position.z);
            Instantiate(_inventoryItem, itemPosition, transform.rotation);
            index++;
        }
    }
}
