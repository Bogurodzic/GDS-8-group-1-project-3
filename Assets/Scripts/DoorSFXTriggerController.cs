using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// temporary solution, needs improvement
// integrate sfx with DoorController if time permits
public class DoorSFXTriggerController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mirror"))
        {
            _audioSource.Play();
        }
    }
}
