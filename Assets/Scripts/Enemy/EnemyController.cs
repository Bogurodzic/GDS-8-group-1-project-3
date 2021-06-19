using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ICharacter
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _healthPickup;
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

    public void Die()
    {
        Instantiate(_healthPickup, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
