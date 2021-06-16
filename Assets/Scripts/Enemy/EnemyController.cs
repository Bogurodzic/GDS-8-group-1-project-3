using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _maxHealth = 5;

    private int _currentHealth;

    
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {

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

    void Die()
    {
        Destroy(gameObject);
    }


}
