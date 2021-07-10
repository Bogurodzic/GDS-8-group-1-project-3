using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatRanged : MonoBehaviour
{
    private enum State
    {
        Patrolling,
        Combat
    }

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _damagePoint;
    //[SerializeField] private Transform _groundCheck;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _spotRange;


    [Header("Speed Parameters")]
    [SerializeField] private float _patrolSpeed = 1f;
    //[SerializeField] private float _chaseSpeed = 4.73f;

    [Header("Combat Parameters")]
    //[SerializeField] private float _sightRange = 2f;
    //[SerializeField] private float _attackDistance = 1f;
    //[SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private int _damage = 1;
    //[SerializeField] private float _pushForce = 10f;

    [Header("Patrol Constraints")]
    [SerializeField] private float _patrolLeftBound;
    [SerializeField] private float _patrolRightBound;
    [SerializeField] private bool _goTowardsLeft = true;

    private State _state;
    private float _priorPosition;

    private void Awake()
    {
        _state = State.Patrolling;
        _priorPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            default:
            case State.Patrolling:
                Patrol();
                SpotPlayer();
                break;
            case State.Combat:
                Shoot();
                break;
        }

        FaceTowardsMovementDirection();
    }

    private void FaceTowardsMovementDirection()
    {
        if (transform.position.x > _priorPosition)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (transform.position.x < _priorPosition)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        _priorPosition = transform.position.x;
    }

    public void Patrol()
    {
        if (transform.position.x <= _patrolLeftBound)
        {
            _goTowardsLeft = false;
        }

        if (transform.position.x >= _patrolRightBound)
        {
            _goTowardsLeft = true;
        }

        if (_goTowardsLeft)
        {
            _rigidbody2D.velocity = new Vector2(-_patrolSpeed, _rigidbody2D.velocity.y);
        }
        else if (!_goTowardsLeft)
        {
            _rigidbody2D.velocity = new Vector2(+_patrolSpeed, _rigidbody2D.velocity.y);
        }
    }

    private void SpotPlayer()
    {
        //OverlapCircle(transform.position, _sightRange, _playerLayer);
        if (_spotRange.IsTouchingLayers(_playerLayer))
        {
            _state = State.Combat;
        }
    }

    void Shoot()
    {
        //shooting logic
    }
}
