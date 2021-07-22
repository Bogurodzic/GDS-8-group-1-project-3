using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonController : MonoBehaviour
{
    [SerializeField] private DoorController _doorController;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Interact");
        _doorController.UnlockDoors();
    }
}
