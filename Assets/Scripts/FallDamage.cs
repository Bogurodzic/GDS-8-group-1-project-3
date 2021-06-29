using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ICharacter>() == null)
        {
            return;
        }
        collision.GetComponent<ICharacter>().Die();
    }
}
