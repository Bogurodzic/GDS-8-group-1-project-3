using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithLightController : DoorController
{
    [SerializeField] private DoorLight[] lightsToUnlock;
    void Update()
    {
        ReloadDoorSprite();
        TryUnlockDoors();
    }
    
    protected void TryUnlockDoors()
    {
        if (CheckIfDoorCanBeUnlocked())
        {
            UnlockDoors();
        }
        else
        {
            LockDoors();
        }
    }
    
    protected void LockDoors()
    {
        _doorLocked = true;
        _boxCollider2D.enabled = true;
    }

    private bool CheckIfDoorCanBeUnlocked()
    {
        bool canBeUnlocked = true;

        foreach (var light in lightsToUnlock)
        {
            if (light._doorLocked)
            {
                canBeUnlocked = false;
            }
        }

        return canBeUnlocked;
    }
}
