using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sun"))
        {
            float endOfSunPosition = _boxCollider2D.bounds.min.y;
            Debug.Log("Sun enter");
            other.gameObject.GetComponent<SunController>().SetNewPosition(endOfSunPosition);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sun"))
        {
            Debug.Log("Sun exit");
            other.gameObject.GetComponent<SunController>().ResetPosition();
        }
    }
}
