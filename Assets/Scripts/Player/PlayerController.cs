using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, ICharacter
{   
    public float maxHealth = 5;
    public float currentHealth;

    [SerializeField] private Animator _animator;
    
    private Vector2 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        currentHealth = maxHealth;
    }

    public void TakeDamage (int _damage)
    {
        Debug.Log("Damage taken " + _damage);
        _animator.SetTrigger("isDamaged");
        currentHealth -= _damage;
        Debug.Log("currentHealth " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Destroy(gameObject);
        //transform.position = _startPosition;
        currentHealth = maxHealth;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        
    }
}
