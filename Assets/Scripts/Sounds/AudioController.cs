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
    [SerializeField] private AudioClip _playerCollectedBlood;
    [SerializeField] private AudioClip _playerIsMovingBox;
    [SerializeField] private AudioClip _boxCrashed;
    [SerializeField] private AudioClip _boxCrashedByPlayer;
    [SerializeField] private AudioClip _boxRespawned;
    [SerializeField] private AudioClip _mirrorCrashed;
    [SerializeField] private AudioClip _mirrorCrashedByPlayer;
    [SerializeField] private AudioClip _collectibleCollected;
    [SerializeField] private AudioClip _collectibleRead;
    [SerializeField] private AudioClip _doorOpen;
    [SerializeField] private AudioClip _lightOn;
    [SerializeField] private AudioClip _playerReadingMonument;
    [SerializeField] private AudioClip _boxFallingDown;
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

    public void playerCollectedBlood()
    {
        playSound(_playerCollectedBlood);
    }
    
    public void playerIsMovingBox()
    {
        _audioSource.loop = true;
        _audioSource.clip = _playerIsMovingBox;
        _audioSource.Play();    
    }

    public bool isPlayerIsMovingBox()
    {
        if (_audioSource.clip == _playerIsMovingBox && _audioSource.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void stopPlayerIsMovingBox()
    {
        _audioSource.Stop();
    }

    public void boxFallingDown()
    {
        playSound(_boxFallingDown);
    }

    public void boxCrashed()
    {
        playSound(_boxCrashed);
    }
    
    public void boxCrashedByPlayer()
    {
        playSound(_boxCrashedByPlayer);
    }

    public void boxRespawned()
    {
        playSound(_boxRespawned);
    }

    public void mirrorCrashed()
    {
        playSound(_mirrorCrashed);
    }
    
    public void mirrorCrashedByPlayer()
    {
        playSound(_mirrorCrashedByPlayer);
    }

    public void collectibleCollected()
    {
        playSound(_collectibleCollected);
    }
    
    public void collectibleRead()
    {
        playSound(_collectibleRead);
    }

    public void doorOpen()
    {
        playSound(_doorOpen);
    }

    public void lightOn()
    {
        playSound(_lightOn);
    }
    
    public void playerReadingMonument()
    {
        _audioSource.loop = true;
        _audioSource.clip = _playerReadingMonument;
        _audioSource.Play();    
    }

    public bool isPlayerReadingMonument()
    {
        if (_audioSource.clip == _playerReadingMonument && _audioSource.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void stopPlayerReadingMonument()
    {
        _audioSource.Stop();
    }
    

    private void playSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
