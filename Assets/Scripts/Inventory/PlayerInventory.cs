using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : GenericSingletonClass<GameManager>
{

    private LinkedList<Collectible> _collectibles = new LinkedList<Collectible>();

    public void AddCollectible(Collectible collectible)
    {
        _collectibles.AddLast(collectible);
    }

    public LinkedList<Collectible> GetCollectibles()
    {
        return _collectibles;
    }
}
