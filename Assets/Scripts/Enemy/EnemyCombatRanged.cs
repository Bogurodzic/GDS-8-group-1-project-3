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
    [SerializeField] private GameObject _projectile;


    [Header("Speed Parameters")]
    [SerializeField] private float _patrolSpeed = 1f;
    [SerializeField] private float _bulletSpeed = 8f;
    //[SerializeField] private float _chaseSpeed = 4.73f;

    //[Header("Combat Parameters")]
    //[SerializeField] private float _sightRange = 2f;
    //[SerializeField] private float _attackDistance = 1f;
    //[SerializeField] private float _attackRange = 0.5f;
    //[SerializeField] private int _damage = 1;
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
                FaceTowardsMovementDirection();
                break;
            case State.Combat:
                FaceTowardsPlayer();
                Shoot();
                break;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(new Vector2(_patrolRightBound, transform.position.y), 0.5f);
        Gizmos.DrawSphere(new Vector2(_patrolLeftBound, transform.position.y), 0.5f);
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
    private void FaceTowardsPlayer()
    {
        if (_player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (_player.transform.position.x >= transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
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
        if (_spotRange.IsTouchingLayers(_playerLayer))
        {
            RaycastHit2D _wallCheck = Physics2D.Raycast(transform.position, _goTowardsLeft ? Vector2.left : Vector2.right, 8f, _groundLayer);
            float _playerDistance = Mathf.Abs(transform.position.x - _player.position.x);

            Debug.Log(gameObject.name + ": distance to wall: " + _wallCheck.distance);
            Debug.Log(gameObject.name + ": distance to player: " + _playerDistance);

            if (_wallCheck.distance > 0 && _wallCheck.distance < _playerDistance)
            {
                return;
            }

            _state = State.Combat;
        }
    }

    void Shoot()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        _animator.SetTrigger("Attack");

    }

    // This is triggered by animation event
    public void Fire()
    {
        if (_player.transform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

            GameObject _bullet = Instantiate(_projectile, transform.position, Quaternion.identity);
            _bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-_bulletSpeed, transform.position.y));
        }
        else if (_player.transform.position.x >= transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            GameObject _bullet = Instantiate(_projectile, transform.position, Quaternion.identity);
            _bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(_bulletSpeed, transform.position.y));
        }

        if (!_spotRange.IsTouchingLayers(_playerLayer))
        {
            _state = State.Patrolling;
        }
    }
}
