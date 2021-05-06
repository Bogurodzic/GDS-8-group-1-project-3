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
    [SerializeField] private float _jumpForce;

    [SerializeField] private LayerMask _platformLayerMask;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    void Start()
    {
        
    }

    void Update()
    {
        HandleMovement();
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
        if (IsGrounded() && (Input.GetKeyDown(_playerJumpFirstKey) || Input.GetKeyDown(_playerJumpSecondKey)))
        {
            return true;
        }
        else
        {
            return false;
        }  
    }

    private void MoveRight()
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

    private void MoveLeft()
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

    private void Stay()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Debug.Log("Jump");
        _rigidbody2D.velocity = Vector2.up * _jumpForce;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f,
            Vector2.down, .1f, _platformLayerMask);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
}
