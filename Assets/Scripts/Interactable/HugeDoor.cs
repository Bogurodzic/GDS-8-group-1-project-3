using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private AudioSource _audio;

    public void Open()
    {
        _animator.SetBool("gateOpened", true);
        _audio.Play();
    }

}
