using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStage : NextStage
{
    [SerializeField] private GameObject _tbc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _tbc.SetActive(true);
        }
    }
}
