using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacter
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _healthPickup;

    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private float _pushForce = 5;

    private int _currentHealth;
    
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (collision.gameObject.transform.eulerAngles == new Vector3(0, 180, 0))
            {
                //odrzucamy gracza w lewo
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * _pushForce);
            }
            else if (collision.gameObject.transform.eulerAngles == new Vector3(0, 0, 0))
            {
                //odrzucamy gracza w prawo
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * _pushForce);
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        _animator.SetTrigger("isDamaged");
        _currentHealth -= _damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(_healthPickup, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
