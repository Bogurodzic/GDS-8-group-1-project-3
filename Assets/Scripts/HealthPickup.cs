using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _health = 1;
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

            Destroy(gameObject);
        }
    }
}
