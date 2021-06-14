using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private int _maxHealth = 5;

    public float _movementSpeed;

    private int _currentHealth;
    private bool _facingLeft = true;
    
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (_facingLeft)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
    }

    public void TakeDamage(int _damage)
    {
        _currentHealth -= _damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void MoveRight()
    {
        _rigidbody2D.velocity = new Vector2(+_movementSpeed, _rigidbody2D.velocity.y);
    }

    public void MoveLeft()
    {
        _rigidbody2D.velocity = new Vector2(-_movementSpeed, _rigidbody2D.velocity.y);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MaximumEnemyDistance"))
        {
            _facingLeft = !_facingLeft;
        }
    }
}
