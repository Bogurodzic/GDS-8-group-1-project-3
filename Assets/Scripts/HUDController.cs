using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _collectibleText;
    [SerializeField] private TextMeshProUGUI _readableObjText;

    [Space(10)]
    [SerializeField] private PlayerController _player;

    [Space(10)]
    [Header("Max Counts")]
    [SerializeField] private byte _maxCollectibleCount;
    [SerializeField] private byte _maxReadableCount;

    private PlayerColletibleController _playerColletibleController;

    private void Start()
    {
        _playerColletibleController = _player.GetComponent<PlayerColletibleController>();
        //_collectibleText.text = _playerColletibleController.collectibleCount + "/" + _maxCollectibleCount;
    }

    void Update()
    {

        UpdateHPText();
        UpdateCollectibleText();
        UpdateReadableObjText();
    }

    private void UpdateCollectibleText()
    {
        _collectibleText.text = _playerColletibleController.collectibleCount + "/" + _maxCollectibleCount;
    }

    private void UpdateHPText()
    {
        _hpText.text = _player.currentHealth + "/" + _player.maxHealth;
    }

    private void UpdateReadableObjText()
    {
        _readableObjText.text = _playerColletibleController.readableCount + "/" + _maxReadableCount;
    }

}
