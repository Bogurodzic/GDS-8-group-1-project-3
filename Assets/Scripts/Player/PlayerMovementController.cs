﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask _platformLayerMask;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private BoxCollider2D _batBoxCollider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _boxMask;

    [Header("Controls")]
    [SerializeField] private KeyCode _playerMoveLeft;
    [SerializeField] private KeyCode _playerMoveRight;
    [SerializeField] private KeyCode _playerMoveDown;
    [SerializeField] private KeyCode _playerJumpFirstKey;
    [SerializeField] private KeyCode _playerJumpSecondKey;

    [Header("Movement Parameters")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _airMovementSpeed;
    [SerializeField][Range(0,1)] private float _horizontalFriction;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _jumpTime;
    [SerializeField] private float _playerMass;

    [Header("Bat Movement Parameters")]
    [SerializeField] private float _batMovementSpeed;
    [SerializeField] private float _doubleJumpForce;
    [SerializeField] [Range(0, 1)] private float _horizontalFrictionNosedive;
    [SerializeField] private float _gravityMultiplier;
    [SerializeField] private float _batMass;

    [HideInInspector] public bool doubleJumpActivated = false;

    private float _jumpTimeCounter;

    private Vector2 _batSize;
    private Vector2 _batOffset;

    private Vector2 _vampireSize;
    private Vector2 _vampireOffset;

    private bool _facingLeft = false;
    private bool _underSun = false;
    private bool _affectedBySun = false;
    private bool _singleJumpActive = false;
    

    private GameObject box;

    void Start()
    {
        LoadColliderSize();
    }

    private void LoadColliderSize()
    {
        _batSize = _batBoxCollider2D.size;
        _batOffset = _batBoxCollider2D.offset;

        _vampireSize = _boxCollider2D.size;
        _vampireOffset = _boxCollider2D.offset;

        _batBoxCollider2D.enabled = false;
    }

    void Update()
    {
        HandleMovement();

        MoveBoxes();
    }

    void MoveBoxes()
    {
        if (transform.eulerAngles == new Vector3(0, 0, 0))
        {
            _facingLeft = false;
        }
        else if (transform.eulerAngles == new Vector3(0, 180, 0))
        {
            _facingLeft = true;
        }

        Physics2D.queriesStartInColliders = true;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), _facingLeft ? Vector2.left : Vector2.right, 0.55f, _boxMask);

        if (hit.collider != null && (hit.collider.gameObject.CompareTag("Mirror") || hit.collider.gameObject.CompareTag("Box")) && Input.GetKey(KeyCode.LeftShift))
        {
            box = hit.collider.gameObject;
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && box)
        {
            if (box.GetComponent<FixedJoint2D>() == null)
            {
                return;
            }
            box.GetComponent<FixedJoint2D>().enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector3(transform.position.x + (_facingLeft ? -0.55f : 0.55f), transform.position.y - 0.5f, transform.position.z));
    }

    private void HandleMovement()
    {
      
        if (CanPlayerJump())
        {
            Jump();
        }

        if (CanPlayerMoveLeft())
        {
            MoveLeft();
        }
        else if (CanPlayerMoveRight())
        {
            MoveRight();
        }
        else if (CanPlayerMoveDown())
        {
            MoveDown();
        }
        else
        {
            if (IsGrounded())
            {
                Stay();
            }
        }

        if (IsGrounded())
        {
            ReloadDoubleJump();
            ReloadUnderSun();
            _affectedBySun = false;
           
            _animator.SetBool("isJumping", false);
            _singleJumpActive = false;
            _jumpTimeCounter = _jumpTime;
        }
        else if (!IsGrounded() && !_animator.GetBool("isBat"))
        {
            _animator.SetBool("isJumping", true);
        }

    }

    private bool CanPlayerMoveRight()
    {
       
        if (Input.GetKey(_playerMoveLeft))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanPlayerMoveLeft()
    {
        if (Input.GetKey(_playerMoveRight))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanPlayerMoveDown()
    {
        if (Input.GetKey(_playerMoveDown) && doubleJumpActivated)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanPlayerJump()
    {
        if ((IsGrounded() || !doubleJumpActivated) && (Input.GetKey(_playerJumpFirstKey) || Input.GetKey(_playerJumpSecondKey)))
        {
            //temporary
            if (Input.GetKeyDown(_playerJumpFirstKey) || Input.GetKeyDown(_playerJumpSecondKey))
            {
                _singleJumpActive = true;
            }

            return true;
        }
        
        else
        {            
            return false;
        }
    }

    private void Jump()
    {

        if (!IsGrounded() && (Input.GetKeyDown(_playerJumpFirstKey) || Input.GetKeyDown(_playerJumpSecondKey)))
        {
            if (!_underSun)
            {
                DoubleJump();
            }
        }
        else
        {
            if (_jumpTimeCounter > 0)
            {
                _jumpTimeCounter -= Time.deltaTime;
                _singleJumpActive = true;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x * _horizontalFriction, _jumpForce);
            }
        }
    }

    private void ReloadDoubleJump()
    {
        if (IsGrounded())
        {
            DeactivateBatMode();
        }
    }

    private void MoveRight()
    {
        if (doubleJumpActivated)
        {
            _rigidbody2D.velocity = new Vector2(-_batMovementSpeed, _rigidbody2D.velocity.y);
            //transform.Translate(new Vector3(-_movementSpeed, 0, 0));
        }
        else if (!_affectedBySun)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (IsGrounded())
            {
                _rigidbody2D.velocity = new Vector2(-_movementSpeed, _rigidbody2D.velocity.y);
                _animator.SetBool("isRunning", true);
            }
            else
            {
                if (_singleJumpActive)
                {
                    _rigidbody2D.velocity += new Vector2(-_movementSpeed * Time.deltaTime, 0);
                    _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -_movementSpeed, +_movementSpeed),
                            _rigidbody2D.velocity.y);
                }
                else
                {
                    _rigidbody2D.velocity += new Vector2(-_airMovementSpeed/4 * Time.deltaTime, 0);
                    _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x/2, -_movementSpeed, +_movementSpeed),
                       _rigidbody2D.velocity.y);
                }

            }
        }
    }

    private void MoveLeft()
    {
        if (doubleJumpActivated)
        {
            _rigidbody2D.velocity = new Vector2(+_batMovementSpeed, _rigidbody2D.velocity.y);
            //transform.Translate(new Vector3(_movementSpeed, 0, 0))
        }
        else if (!_affectedBySun)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (IsGrounded())
            {
                _rigidbody2D.velocity = new Vector2(+_movementSpeed, _rigidbody2D.velocity.y);
                _animator.SetBool("isRunning", true);
            }
            else
            {
                if (_singleJumpActive)
                {
                    _rigidbody2D.velocity += new Vector2(+_movementSpeed * Time.deltaTime, 0);
                    _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -_movementSpeed, +_movementSpeed),
                         _rigidbody2D.velocity.y);

                }
                else
                {
                    _rigidbody2D.velocity += new Vector2(+_airMovementSpeed/4 * Time.deltaTime, 0);
                    _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x/2, -_movementSpeed, +_movementSpeed),
                        _rigidbody2D.velocity.y);

                }
            }
        }
    }

    private void MoveDown()
    {
        _rigidbody2D.velocity = new Vector2 (_rigidbody2D.velocity.x * _horizontalFrictionNosedive, -_batMovementSpeed);
    }

    private void Stay()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        _animator.SetBool("isRunning", false);
    }

    private void DoubleJump()
    {
        ActivateBatMode();
        _rigidbody2D.velocity = Vector2.up * _doubleJumpForce;
        _animator.SetBool("isJumping", false);
        _animator.SetBool("isBat", true);
        _singleJumpActive = false;
    }

    public void DeactivateBatMode(bool deactivateDoubleJump = true)
    {
        if (deactivateDoubleJump)
        {
            doubleJumpActivated = false;
        }
        _rigidbody2D.mass = _playerMass;
        _rigidbody2D.gravityScale = _gravityScale;
        _animator.SetBool("isBat", false);
        ReloadBoxCollider();
    }

    private void ActivateBatMode()
    {
        doubleJumpActivated = true;
        _rigidbody2D.mass = _batMass;
        _rigidbody2D.gravityScale = 1f / _gravityMultiplier;
        ReloadBoxCollider();
    }

    private bool IsGrounded()
    {
        if (doubleJumpActivated)
        {
            RaycastHit2D raycastHit2Dplatform = Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x / 2, _boxCollider2D.bounds.size.y), 0f,
                Vector2.down, .8f, _platformLayerMask);
            RaycastHit2D raycastHit2Dbox = Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x / 2, _boxCollider2D.bounds.size.y), 0f,
                Vector2.down, .8f, _boxMask);

            return (raycastHit2Dplatform.collider != null || raycastHit2Dbox.collider != null);  
        }
        else
        {
            RaycastHit2D raycastHit2Dplatform = Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x / 2, _boxCollider2D.bounds.size.y), 0f,
                Vector2.down, .3f, _platformLayerMask);
            RaycastHit2D raycastHit2Dbox = Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.bounds.size.x / 2, _boxCollider2D.bounds.size.y), 0f,
                Vector2.down, .3f, _boxMask);

            return (raycastHit2Dplatform.collider != null || raycastHit2Dbox.collider != null);
        }

    }

    private void ReloadBoxCollider()
    {
        if (doubleJumpActivated)
        {
            _boxCollider2D.size = _batSize;
            _boxCollider2D.offset = _batOffset;
        }
        else
        {
            _boxCollider2D.size = _vampireSize;
            _boxCollider2D.offset = _vampireOffset;
        }
    }

    public void HandleSunEffect()
    {
        if (doubleJumpActivated)
        {
            _affectedBySun = true;
            DeactivateBatMode(false);
            ReloadBoxCollider();
            Stay();
        }
        else
        {
            _underSun = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }

    private void ReloadUnderSun()
    {
        _underSun = false;
    }
    
    
}
