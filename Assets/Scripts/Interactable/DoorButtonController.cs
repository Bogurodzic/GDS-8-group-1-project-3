using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonController : MonoBehaviour
{
    [SerializeField] private DoorController _doorController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _lockedButton;
    [SerializeField] private Sprite _unlockedButton;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        _spriteRenderer.sprite = _unlockedButton;
        _doorController.UnlockDoors();
    }
    
}
