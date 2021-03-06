using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryShowcase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private AudioController _audioController;
    public void LoadItem(Collectible item)
    {
        _audioController.collectibleRead();
        _name.text = item.GetName();
        _image.sprite = item.GetSprite();
        _description.text = item.GetDescription();
    }
}
