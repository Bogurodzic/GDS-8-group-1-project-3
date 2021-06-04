using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
            
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
        Debug.Log(transform.position.y);
        Debug.Log(newY);
        Debug.Log(yPosition);

        float finalY = (newY - transform.position.y) + (yPosition - transform.position.y) + transform.position.y;
        
        transform.position = new Vector3(transform.position.x, finalY, transform.position.z);
        
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, _initialYPosition, transform.position.z);
    }
}
