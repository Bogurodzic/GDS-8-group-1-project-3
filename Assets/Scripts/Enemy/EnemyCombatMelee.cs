using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatMelee : MonoBehaviour
{
    private enum State
    {
        Patrolling,
        Combat
    }
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _player;

    [Header("Speed Parameters")]
    [SerializeField] private float _patrolSpeed = 1f;
    [SerializeField] private float _chaseSpeed = 4.73f;

    [Header("Combat Parameters")]
    [SerializeField] private float _sightRange = 2f;
    [SerializeField] private float _attackRange = 1f;

    [Header("Patrol Constraints")]
    [SerializeField] private float _patrolLeftBound;
    [SerializeField] private float _patrolRightBound;
    [SerializeField] private bool _goTowardsLeft = true;

    private State _state;

    private void Awake()
    {
        _state = State.Patrolling;
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
                ChasePlayer();
                break;
        }
    }

    public void Patrol()
    {
        if(transform.position.x <= _patrolLeftBound)
        {
            _goTowardsLeft = false;
        }
        if(transform.position.x >= _patrolRightBound)
        {
            _goTowardsLeft = true;
        }


        if (_goTowardsLeft)
        {
            _rigidbody2D.velocity = new Vector2(-_patrolSpeed, _rigidbody2D.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (!_goTowardsLeft)
        {
            _rigidbody2D.velocity = new Vector2(+_patrolSpeed, _rigidbody2D.velocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void SpotPlayer()
    {
        Collider2D spotRange = Physics2D.OverlapCircle(transform.position, _sightRange, _playerLayer);
        if (spotRange)
        {
            _state = State.Combat;
        }
    }

    void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, _player.position) <= _attackRange)
        {
            Attack();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _chaseSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        //attack logic
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _sightRange);
        Gizmos.DrawSphere(new Vector2(_patrolRightBound, transform.position.y), 0.5f);
        Gizmos.DrawSphere(new Vector2(_patrolLeftBound, transform.position.y), 0.5f);
    }
}