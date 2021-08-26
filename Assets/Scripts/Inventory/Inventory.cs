using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    public static bool IsInventoryOpened = false;

    public static void SetInventory(bool isOpened)
    {
        IsInventoryOpened = isOpened;
    }
}
