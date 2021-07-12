using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundLayers;

    private void FixedUpdate()
    {
        FallDown();
    }

    void FallDown()
    {
        RaycastHit2D groundCheck = Physics2D.BoxCast(_groundPoint.position, new Vector2(_boxCollider2D.bounds.size.x, _boxCollider2D.bounds.size.y/2), 0f, Vector2.down, _groundLayers);
        if (groundCheck)
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
        }
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
