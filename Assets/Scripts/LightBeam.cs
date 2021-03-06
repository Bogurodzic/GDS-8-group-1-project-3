using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _platformLayers;
    [SerializeField] private LayerMask _doorButtonLayer;
    [SerializeField] private LayerMask _mirrorLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _distanceRay = 100f;
    [SerializeField] private float _reflectedRayDistance = 10f;
    [SerializeField] private GameObject _playerController;
    
    [SerializeField] private int _firstTimeDamageToPlayerDelay;
    [SerializeField] private int _regularTimeDamageToPlayerDelay;
    [SerializeField] private int _damageToPlayer;
    [SerializeField] private AudioController _audioController;

    private bool _playerIsAffectedBySun = false;
    private bool _damageDealingProcessStarded = false;
    private bool _firstDamageDealed = false;
    
    private void FixedUpdate()
    {
        CastLight();
        TryDealDamageToPlayer();
    }

    private void CastLight()
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.right, _distanceRay, _platformLayers);
        RaycastHit2D _hitPlayer = Physics2D.BoxCast(transform.position, new Vector2(0.6f, 0.6f), 0, transform.right, _distanceRay, _playerLayer);

        //It can be enabled if we want to use light to open door without reflecting it

        RaycastHit2D _hitDoor = Physics2D.BoxCast(transform.position, new Vector2(0.7f, 0.7f), 0, transform.right, _distanceRay, _doorButtonLayer);
        InteractWithDoors(_hitDoor);

        float normalHitDistance = 0;

        if (_hit)
        {
            normalHitDistance = _hit.distance;
        }

        float playerHitDistance = 0;
            
        if (_hitPlayer)
        {
            playerHitDistance = _hitPlayer.distance;
        }

        if (_hit && _hitPlayer && playerHitDistance < normalHitDistance)
        {
            TouchPlayer(_hitPlayer);
        } else if (!_hit && _hitPlayer)
        {
            TouchPlayer(_hitPlayer);
        }
        else
        {
            ResetSunEffect();
        }
        
        if (_hit)
        {
            DrawBeam(transform.position, _hit.point);
            if (_hit.collider.tag == "Mirror")
            {
                Vector2 reflectedPositionEndPoint =
                    Vector2.Reflect(
                        (_hit.point - new Vector2(transform.position.x, transform.position.y)) * (_distanceRay/2),
                        _hit.normal);
                Vector2 reflectedPosition = Vector2.MoveTowards(_hit.point, reflectedPositionEndPoint, _reflectedRayDistance);
                
                //Raycast to find door button
                RaycastHit2D _hitDoorReflected = Physics2D.BoxCast(_hit.point, new Vector2(0.7f, 0.7f), 0, reflectedPositionEndPoint,
                    _reflectedRayDistance + 30, _doorButtonLayer);
                InteractWithDoors(_hitDoorReflected);

                
                _lineRenderer.SetPosition(2, reflectedPosition);
                RaycastHit2D _hitPlayerMirrored = Physics2D.Raycast(_hit.point, reflectedPositionEndPoint,
                    _reflectedRayDistance, _playerLayer);
                TouchPlayer(_hitPlayerMirrored);
            }
            else
            {
                _lineRenderer.SetPosition(2, _lineRenderer.GetPosition(1));
            }
            
        }
        else
        {
            DrawBeam(transform.position, transform.transform.right * _distanceRay);
            _lineRenderer.SetPosition(2, _lineRenderer.GetPosition(1));
        }

    }
    

    private void InteractWithDoors(RaycastHit2D hitDoor)
    {

        if (hitDoor)
        {
            DoorButtonController doorButtonController = hitDoor.collider.gameObject.GetComponent<DoorButtonController>();
            doorButtonController.Interact();
        }
    }

    private void TouchPlayer(RaycastHit2D hitPlayer)
    {
        if (hitPlayer)
        {
            hitPlayer.collider.gameObject.GetComponent<PlayerMovementController>().HandleSunEffect();
            _playerIsAffectedBySun = true;
            
            if (!_audioController.isPlayerIsUnderSun())
            {
                _audioController.playerIsUnderSun();
            }
            
            hitPlayer.collider.gameObject.GetComponent<PlayerController>().GetAnimator().SetBool("underSun", true);
        }
        else
        {
            ResetSunEffect();
        }
    }

    private void ResetSunEffect()
    {
        if (_playerIsAffectedBySun)
        {
            _playerMovementController.doubleJumpActivated = false;
            _playerMovementController.ReloadUnderSun(); 
            _playerController.GetComponent<PlayerController>().GetAnimator().SetBool("underSun", false);
            _audioController.stopPlayerIsUnderSun();
            _playerIsAffectedBySun = false;
        }  
    }

    private void DrawBeam(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawRay(transform.position, transform.right);
    }

    private void TryDealDamageToPlayer()
    {
        if (_playerIsAffectedBySun && !_damageDealingProcessStarded && !_firstDamageDealed)
        {
            //Debug.Log("TryDealDamageToPlayer 1");
            _damageDealingProcessStarded = true;
            Invoke("DealDamage", _firstTimeDamageToPlayerDelay);
        } else if (_playerIsAffectedBySun && !_damageDealingProcessStarded && _firstDamageDealed)
        {
            //Debug.Log("TryDealDamageToPlayer 2");

            _damageDealingProcessStarded = true;
            Invoke("DealDamage", _regularTimeDamageToPlayerDelay);
        }
        else if (!_playerIsAffectedBySun)
        {
            ResetDamagingPlayer();
        }
    }

    private void ResetDamagingPlayer()
    {
        CancelInvoke();
        _damageDealingProcessStarded = false;
        _firstDamageDealed = false;
    }

    private void DealDamage()
    {
        //Debug.Log("DEAL DAMAGE 1");
        if (_playerIsAffectedBySun && !_firstDamageDealed)
        {
            //Debug.Log("DEAL DAMAGE 2");

            //_playerController.TakeDamage(_damageToPlayer);
            _playerController.GetComponent<ICharacter>().TakeDamage(_damageToPlayer);

            _firstDamageDealed = true;
        } else if (_playerIsAffectedBySun && _firstDamageDealed)
        {
            //Debug.Log("DEAL DAMAGE 3");

            //_playerController.TakeDamage(_damageToPlayer);
            _playerController.GetComponent<ICharacter>().TakeDamage(_damageToPlayer);

        }
        
        _damageDealingProcessStarded = false;

    }
}
