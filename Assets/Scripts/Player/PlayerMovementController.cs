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
    [SerializeField] private float _jumpForce;

    [SerializeField] private LayerMask _platformLayerMask;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    void Start()
    {
        
    }

    void Update()
    {
        if (CanPlayerMoveLeft())
        {
            MoveLeft();
        }

        if (CanPlayerMoveRight())
        {
            MoveRight();
        }

        if (CanPlayerJump())
        {
            Jump();
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
        transform.Translate(Vector3.left * Time.deltaTime * _movementSpeed);
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _movementSpeed);
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
