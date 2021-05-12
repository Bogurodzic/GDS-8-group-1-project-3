using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{

    private float _initialYPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _initialYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector2.down, Color.green, 999);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Box"))
        {
            Debug.Log("BOX");
        }
    }

    public void SetNewPosition(float yPosition)
    {
        float newY = gameObject.GetComponent<BoxCollider2D>().bounds.max.y;
        transform.position = new Vector3(transform.position.x, 18.33f, transform.position.z);
        
        Debug.Log(transform.position);
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, _initialYPosition, transform.position.z);
    }
}
