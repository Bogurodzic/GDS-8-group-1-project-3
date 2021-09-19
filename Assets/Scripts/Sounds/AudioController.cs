using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _playerMissedAttackWithWeapon;
    [SerializeField] private AudioClip _playerHittedAttackWithWeapon;
    [SerializeField] private AudioClip _playerRecievedDamageInHumanForm;
    
    [SerializeField] private AudioClip _playerJumpedFromGround;
    [SerializeField] private AudioClip _playerLandedOnHumanForm;
    [SerializeField] private AudioClip _playerTransformedToBat;
    [SerializeField] private AudioClip _playerLandedOnBatForm;
    [SerializeField] private AudioClip _playerIsUnderSun;
    [SerializeField] private AudioClip _playerRecievedDamageInBatForm;
    [SerializeField] private AudioClip _playerDeath;
    [SerializeField] private AudioClip _batFlying;


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

    public void playerJumpedFromGround()
    {
        playSound(_playerJumpedFromGround);
    }

    public void playerLandedOnHumanForm()
    {
        playSound(_playerLandedOnHumanForm);
    }

    public void playerTransformedToBat()
    {
        playSound(_playerTransformedToBat);
    }

    public void batFlying()
    {
        _audioSource.loop = true;
        _audioSource.clip = _batFlying;
        _audioSource.Play();
    }

    public bool isBatFlying()
    {
        if (_audioSource.clip == _batFlying && _audioSource.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void stopBatFlying()
    {
        _audioSource.Stop();
    }

    public void playerLandedOnBatForm()
    {
        playSound(_playerLandedOnBatForm);
    }

    public void playerIsUnderSun()
    {
        _audioSource.loop = true;
        _audioSource.clip = _playerIsUnderSun;
        _audioSource.Play();    
    }

    public bool isPlayerIsUnderSun()
    {
        if (_audioSource.clip == _playerIsUnderSun && _audioSource.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void stopPlayerIsUnderSun()
    {
        _audioSource.Stop();
    }

    public void playerRecievedDamageInBatForm()
    {
        playSound(_playerRecievedDamageInBatForm);
    }

    public void playerDeath()
    {
        playSound(_playerDeath);
    }
    

    private void playSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
