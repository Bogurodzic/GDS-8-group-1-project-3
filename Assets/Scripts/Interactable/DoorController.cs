using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Sprite _lockedDoorSprite;
    [SerializeField] private Sprite _openedDoorSprite;
    [SerializeField] private bool _doorLocked;
    void Start()
    {
        
    }

    void Update()
    {
        ReloadDoorSprite();
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
    }

    public void LockDoors()
    {
        _doorLocked = true;
        _boxCollider2D.enabled = true;
    }
}
