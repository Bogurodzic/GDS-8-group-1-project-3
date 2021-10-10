using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private float _destroyAfterTime = 1;
    [SerializeField] private AudioController _audioController;

    public void Start()
    {
        _audioController = GameObject.Find("Player").GetComponent<AudioController>();
        Invoke("DestroyAfterSetTime", _destroyAfterTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            if (collision.gameObject.GetComponent<PlayerController>().currentHealth < collision.gameObject.GetComponent<PlayerController>().maxHealth)
            {
                collision.gameObject.GetComponent<PlayerController>().currentHealth += _health;

                if (collision.gameObject.GetComponent<PlayerController>().currentHealth > collision.gameObject.GetComponent<PlayerController>().maxHealth)
                {
                    collision.gameObject.GetComponent<PlayerController>().currentHealth = collision.gameObject.GetComponent<PlayerController>().maxHealth;
                }
            }

            _audioController.playerCollectedBlood();
            Destroy(gameObject);
        }
    }

    private void DestroyAfterSetTime()
    {
        Destroy(gameObject);
    }
}
