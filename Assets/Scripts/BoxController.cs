using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sun"))
        {
            Debug.Log("Sun enter");
            other.gameObject.GetComponent<SunController>().SetNewPosition(transform.position.y);
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
