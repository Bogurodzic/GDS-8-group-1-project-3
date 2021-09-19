using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _playerMissedAttackWithWeapon;
    [SerializeField] private AudioClip _playerHittedAttackWithWeapon;
    [SerializeField] private AudioClip _playerRecievedDamageInHumanForm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerMissedAttackWithWeapon()
    {
        playSound(_playerMissedAttackWithWeapon);
    }

    public void playerAttackedOpponentWithWeapon()
    {
        playSound(_playerHittedAttackWithWeapon);
    }

    public void playerRecievedDamageInHumanForm()
    {
        playSound(_playerRecievedDamageInHumanForm);
    }
    

    private void playSound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.loop = false;
        _audioSource.Play();
    }
}
