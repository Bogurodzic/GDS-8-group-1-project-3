    using System;
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
    [SerializeField] private AudioController _audioController;
    [SerializeField] private GameObject _puffFX;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 9) && _playerMovementController.doubleJumpActivated)
        {
            PlayPuffFX();
        }
    }

    public void TakeDamage (int _damage)
    {
        if (isInvulnerable)
        {
            return;
        }
        _playerMovementController.DeactivateBatMode(true);
        _animator.SetTrigger("isDamaged");
        currentHealth -= _damage;

        if (_animator.GetBool("isBat"))
        {
            if (_audioController.isBatFlying())
            {
                _audioController.stopBatFlying();
            }
            _audioController.playerRecievedDamageInBatForm();
        } else
        {
            _audioController.playerRecievedDamageInHumanForm();
        }

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
        StartCoroutine(DamageFlicker());
    }

    private IEnumerator DamageFlicker()
    {
        for (int i = 0; i < 3; i++)
        {
            _sprite.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSecondsRealtime(_invulnerableTime/3);
            _sprite.color = Color.white;
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
        _audioController.playerDeath();
        currentHealth = maxHealth;

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        
    }

    public void PlayPuffFX()
    {
        GameObject puff = Instantiate(_puffFX, transform.position, transform.rotation);
        Destroy(puff, 0.6f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            HugeDoor hugeDoor = other.gameObject.GetComponent<HugeDoor>();
            hugeDoor.Open();
        }
    }
}
