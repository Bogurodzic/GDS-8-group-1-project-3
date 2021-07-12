using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter
{   
    public float maxHealth = 5;
    public float currentHealth;

    [SerializeField] private Animator _animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage (int _damage)
    {
        _animator.SetTrigger("isDamaged");
        currentHealth -= _damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
