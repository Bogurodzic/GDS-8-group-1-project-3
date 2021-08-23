using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    private bool _isActive = false;
    private Collectible _collectible;
        

    void Start()
    {
        
    }

    void Update()
    {
        ReloadColor();
    }

    private void ReloadColor()
    {
        if (!_isActive)
        {
            _itemImage.color = Color.white;
        }
        else
        {
            _itemImage.color = Color.yellow;

        }
    }

    public void AddCollectible(Collectible collectible)
    {
        _collectible = collectible;
    }

    public Collectible GetCollectible()
    {
        return _collectible;
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }
}
