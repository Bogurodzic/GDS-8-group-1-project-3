using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private KeyCode _playerMoveLeft;
    [SerializeField] private KeyCode _playerMoveRight;
    [SerializeField] private KeyCode _playerJumpFirstKey;
    [SerializeField] private KeyCode _playerJumpSecondKey;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _airMovementSpeed;
    [SerializeField] private float _batMovementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _doubleJumpForce;
    [SerializeField] private float _gravityMultiplier;

    [SerializeField] private LayerMask _platformLayerMask;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private BoxCollider2D _batBoxCollider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _boxMask;

    private bool _doubleJumpActivated = false;

    private Vector2 _batSize;
    private Vector2 _batOffset;

    private Vector2 _vampireSize;
    private Vector2 _vampireOffset;

    private bool _facingLeft = false;

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

        Physics2D.queriesStartInColliders = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _facingLeft ? Vector2.left : Vector2.right, 0.35f, _boxMask);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Box") && Input.GetKeyDown(KeyCode.LeftShift))
        {
            box = hit.collider.gameObject;
            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        } else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,  new Vector3(transform.position.x + (_facingLeft ? -0.35f : 0.35f), transform.position.y, transform.position.z));
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
        } else if (CanPlayerMoveRight())
        {
            MoveRight();
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
    
    private bool CanPlayerJump()
    {
        if ((IsGrounded() || !_doubleJumpActivated) && (Input.GetKeyDown(_playerJumpFirstKey) || Input.GetKeyDown(_playerJumpSecondKey)))
        {
            return true;
        }
        else
        {
            return false;
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
        if (_doubleJumpActivated)
        {
            _rigidbody2D.velocity = new Vector2(-_batMovementSpeed, _rigidbody2D.velocity.y);
        }
        else
        {
            if (IsGrounded())
            {
                _rigidbody2D.velocity = new Vector2(-_movementSpeed, _rigidbody2D.velocity.y);
            }
            else
            {
                _rigidbody2D.velocity += new Vector2(-_movementSpeed * _airMovementSpeed * Time.deltaTime, 0);
                _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -_movementSpeed, +_movementSpeed),
                    _rigidbody2D.velocity.y);
            }         
        }
    }

    private void MoveLeft()
    {
        if (_doubleJumpActivated)
        {
            _rigidbody2D.velocity = new Vector2(+_batMovementSpeed, _rigidbody2D.velocity.y);
        }
        else
        {
            if (IsGrounded())
            {
                _rigidbody2D.velocity = new Vector2(+_movementSpeed, _rigidbody2D.velocity.y);
            }
            else
            {
                _rigidbody2D.velocity += new Vector2(+_movementSpeed * _airMovementSpeed * Time.deltaTime, 0);
                _rigidbody2D.velocity = new Vector2(Mathf.Clamp(_rigidbody2D.velocity.x, -_movementSpeed, +_movementSpeed),
                    _rigidbody2D.velocity.y);
            }
        }

    }

    private void Stay()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {        

        if (!IsGrounded())
        {
            DoubleJump();
        }
        else
        {
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
        }
    }

    private void DoubleJump()
    {
        ActivateBatMode();
        _rigidbody2D.velocity = Vector2.up * _doubleJumpForce;
    }

    private void DeactivateBatMode()
    {
        _doubleJumpActivated = false;
        _rigidbody2D.mass = 40;
        _rigidbody2D.gravityScale = 1f;
        _animator.SetBool("isBat", false);
        ReloadBoxCollider();
    }

    private void ActivateBatMode()
    {
        _doubleJumpActivated = true;
        _rigidbody2D.mass = 1;
        _rigidbody2D.gravityScale = 1f / _gravityMultiplier;
        _animator.SetBool("isBat", true);
        ReloadBoxCollider();
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f,
            Vector2.down, .01f, _platformLayerMask);
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }

    private void ReloadBoxCollider()
    {
        if (_doubleJumpActivated)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sun"))
        {
            DeactivateBatMode();
            ReloadBoxCollider();
            Stay();
        }
    }



}
