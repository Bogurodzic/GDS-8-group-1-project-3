using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ICharacter>() == null || collision.gameObject.tag == "Player" || collision.gameObject.layer == 9)
        {
            return;
        }
        collision.GetComponent<ICharacter>().Die();
    }
}
