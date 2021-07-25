using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingObjectController : MonoBehaviour
{
    public int respawnTimer;
    private Vector2 _initialPosition;
    
    void Start()
    {
        SetInitialPosition();
    }

    void Update()
    {
        
    }

    private void SetInitialPosition()
    {
        _initialPosition = gameObject.transform.position;
    }

    public void StartRespawningObject()
    {
        SetObjectInactive();
        Invoke("RespawnObject", respawnTimer);
    }

    private void RespawnObject()
    {
        SetObjectActive();
        PlaceObjectAtInitialPosition();
    }

    private void SetObjectInactive()
    {
        gameObject.SetActive(false);
    }

    private void SetObjectActive()
    {
        gameObject.SetActive(true);
    }

    private void PlaceObjectAtInitialPosition()
    {
        gameObject.transform.position = _initialPosition;
    }
}
