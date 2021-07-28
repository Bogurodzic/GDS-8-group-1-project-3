using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private PlayerController _player;

    // Update is called once per frame
    void Update()
    {
        _hpText.text = "Current HP: " + _player.currentHealth;
    }
}
