using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour, ICharacter
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private GameObject _killingArea;

    [SerializeField] private float _maxHP = 6;
    private float _currentHP;

    private void Start()
    {
        _currentHP = _maxHP;    
    }

    private void FixedUpdate()
    {
        FallDown();
    }

    void Update()
    {
        if (_rigidbody.velocity.y < -0.1f && _killingArea != null)
        {
            _killingArea.SetActive(true);
        }
        else if (_killingArea != null)
        {
            _killingArea.SetActive(false);
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

    public void TakeDamage (int _damage)
    {
        _currentHP -= _damage;

        if (_currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (GetComponent<DestroyingObjectController>() == null)
        {
            return;
        }

        GetComponent<DestroyingObjectController>().StartRespawningObject();
        _currentHP = _maxHP;
    }

    void FallDown()
    {
        RaycastHit2D groundCheck = Physics2D.BoxCast(_groundPoint.position, new Vector2(_boxCollider2D.bounds.size.x, _boxCollider2D.bounds.size.y / 2), 0f, Vector2.down, _groundLayers);
        if (groundCheck)
        {
            _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
        }
    }
}
