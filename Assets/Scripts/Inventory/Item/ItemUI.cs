using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Sprite _itemImage;
    [SerializeField] private Sprite _itemImageOpen;
    [SerializeField] private GameObject _itemName;
    [SerializeField] private Image _imageSource;
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
            _itemName.SetActive(false);
            _imageSource.sprite = _itemImage;
        }
        else
        {
            _itemName.SetActive(true);
            _imageSource.sprite = _itemImageOpen;
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
