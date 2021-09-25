using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private PlayerController _player;

    [Header("Text Strings")]
    [SerializeField] private string _hpTextString;

    // Update is called once per frame
    void Update()
    {
        _hpText.text = _hpTextString + _player.currentHealth;
    }
}
