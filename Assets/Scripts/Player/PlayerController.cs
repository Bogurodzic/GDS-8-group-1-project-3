    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, ICharacter
{
    public float maxHealth = 5;
    public float currentHealth;

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovementController _playerMovementController;

    [SerializeField] private float _invulnerableTime;

    private Vector2 _startPosition;
    [HideInInspector] public bool isInvulnerable;

    private SpriteRenderer _sprite;

    void Start()
    {

        _startPosition = transform.position;
        currentHealth = maxHealth;
        isInvulnerable = false;
        _sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        IgnoreEnemyCollider();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            InvulnerableFX();
        }
    }

    public void TakeDamage (int _damage)
    {
        if (isInvulnerable)
        {
            return;
        }
        Debug.Log("Damage taken " + _damage);
        _playerMovementController.DeactivateBatMode(true);
        _animator.SetTrigger("isDamaged");
        currentHealth -= _damage;
        Debug.Log("currentHealth " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(GetInvulnerable());
        }
    }

    private IEnumerator GetInvulnerable()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(_invulnerableTime);
        isInvulnerable = false;
    }

    private void InvulnerableFX()
    {
        Color tmp = _sprite.color;
        if (tmp.a == 255)
        {
            tmp.a = 0;
            _sprite.color = tmp;
        } 
        else if (tmp.a == 0)
        {
            tmp.a = 255;
            _sprite.color = tmp;
        }
    }

    private void IgnoreEnemyCollider()
    {
        if (isInvulnerable)
        {
            Physics2D.IgnoreLayerCollision(11, 12, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(11, 12, false);
        }
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public void Die()
    {
        //Destroy(gameObject);
        //transform.position = _startPosition;
        currentHealth = maxHealth;

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        
    }
}
