using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RespawnBox(collision);
    }

    void RespawnBox(Collider2D col)
    {
        if (col.CompareTag("BoxRespawn"))
        {
            col.gameObject.GetComponent<DestroyingObjectController>().StartRespawningObject();
        }
    }
}
