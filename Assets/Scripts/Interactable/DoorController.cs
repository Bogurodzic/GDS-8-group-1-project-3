using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DoorController : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] protected BoxCollider2D _boxCollider2D;
    [SerializeField] protected Sprite _lockedDoorSprite;
    [SerializeField] protected Sprite _openedDoorSprite;
    [SerializeField] public bool _doorLocked;
    [SerializeField] protected float _lockDelay = 0.5f;
    
    protected String _lastUnlockedDoorId;
    protected String _doorIdToCheck;
    protected bool _doorUnderChecking = false;
    void Start()
    {
        
    }

    void Update()
    {
        ReloadDoorSprite();
        TryUnlockDoors();
    }

    protected void ReloadDoorSprite()
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
        if (_boxCollider2D)
        {
            _boxCollider2D.enabled = false;
        }
        _lastUnlockedDoorId = CreateRandomIdString();
    }

    protected void TryUnlockDoors()
    {
        if (!_doorUnderChecking)
        {
            _doorIdToCheck = _lastUnlockedDoorId;
            _doorUnderChecking = true;
            Invoke("LockDoors", _lockDelay);
        }
    }
    
    protected void LockDoors()
    {
        if (CheckIfDoorsCanBeLocked())
        {
            _doorLocked = true;
            if (_boxCollider2D)
            {
                _boxCollider2D.enabled = true;
            }
        }

        _doorUnderChecking = false;
    }

    protected bool CheckIfDoorsCanBeLocked()
    {
        return _lastUnlockedDoorId == _doorIdToCheck;
    }

    protected String CreateRandomIdString()
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
