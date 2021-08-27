using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DoorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite _lockedDoorSprite;
    [SerializeField] private Sprite _openedDoorSprite;
    [SerializeField] private bool _doorLocked;
    [SerializeField] private float _lockDelay = 0.5f;
    
    private String _lastUnlockedDoorId;
    private String _doorIdToCheck;
    private bool _doorUnderChecking = false;
    void Start()
    {
        
    }

    void Update()
    {
        ReloadDoorSprite();
        TryUnlockDoors();
    }

    void ReloadDoorSprite()
    {
        if (_doorLocked)
        {
            _spriteRenderer.sprite = _lockedDoorSprite;
        }
        else
        {
            _spriteRenderer.sprite = _openedDoorSprite;
        } 
    }

    public void UnlockDoors()
    {
        _doorLocked = false;
        _boxCollider2D.enabled = false;
        _lastUnlockedDoorId = CreateRandomIdString();
    }

    private void TryUnlockDoors()
    {
        if (!_doorUnderChecking)
        {
            _doorIdToCheck = _lastUnlockedDoorId;
            _doorUnderChecking = true;
            Invoke("LockDoors", _lockDelay);
        }
    }
    
    private void LockDoors()
    {
        if (CheckIfDoorsCanBeLocked())
        {
            _doorLocked = true;
            _boxCollider2D.enabled = true;  
        }

        _doorUnderChecking = false;
    }

    private bool CheckIfDoorsCanBeLocked()
    {
        return _lastUnlockedDoorId == _doorIdToCheck;
    }

    private String CreateRandomIdString()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);
        return finalString;
    }
}
