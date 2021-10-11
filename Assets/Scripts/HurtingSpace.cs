using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtingSpace : MonoBehaviour
{
    [SerializeField] private float _timeInterval = 2f;
    [SerializeField] private int _damage = 1;
    private float _collisionTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisionTime = 0f;
        Hurt(collision);
        DestroyBox(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_collisionTime < _timeInterval)
        {
            _collisionTime += Time.deltaTime;
        }
        else
        {
            Hurt(collision);
            _collisionTime = 0f;
        }
    }

    private void Hurt(Collider2D character)
    {
        if (character.GetComponent<ICharacter>() == null && character.gameObject.layer != 9)
        {
            return;
        }
        character.GetComponent<ICharacter>().TakeDamage(_damage);
    }
    
    private void DestroyBox(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Mirror"))
        {
            collider2D.gameObject.GetComponent<DestroyingObjectController>().StartRespawningObject();
        }
    }
}
