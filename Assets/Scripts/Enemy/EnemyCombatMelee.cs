using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatMelee : MonoBehaviour
{
    private enum State
    {
        Patrolling,
        Attention,
        Combat
    }

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _damagePoint;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _spotRange;
    [SerializeField] private GameObject _attentionMark;


    [Header("Speed Parameters")]
    [SerializeField] private float _patrolSpeed = 1f;
    [SerializeField] private float _chaseSpeed = 4.73f;

    [Header("Combat Parameters")]
    //[SerializeField] private float _sightRange = 2f;
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private float _attentionTime = 2f;

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
            case State.Attention:
                StayAlert();
                break;
            case State.Combat:
                ChasePlayer();
                FaceTowardsPlayer();
                break;
        }

        if (_state == State.Attention && _spotRange.IsTouchingLayers(_playerLayer))
        {
            StopAllCoroutines();
            _attentionMark.SetActive(false);
            _state = State.Patrolling;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(new Vector2(_patrolRightBound, transform.position.y), 0.5f);
        Gizmos.DrawSphere(new Vector2(_patrolLeftBound, transform.position.y), 0.5f);
        Gizmos.DrawWireSphere(_damagePoint.position, _attackRange);
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
            RaycastHit2D _wallCheck = Physics2D.Raycast(transform.position, _goTowardsLeft ? Vector2.left: Vector2.right, 6f, _groundLayer);
            float _playerDistance = Mathf.Abs(transform.position.x - _player.position.x);
            
            Debug.Log(gameObject.name + ": distance to wall: " + _wallCheck.distance);
            Debug.Log(gameObject.name + ": distance to player: " + _playerDistance);

            if (_wallCheck.distance > 0 &&_wallCheck.distance < _playerDistance)
            {
                return;
            }

            _state = State.Combat;
        }
    }

    private void ChasePlayer()
    {
        if (_player == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, _player.position) <= _attackDistance)
        {
            Attack();
        }
        else
        {

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_player.position.x, transform.position.y), _chaseSpeed * Time.deltaTime);

            Collider2D _grounded = Physics2D.OverlapCircle(_groundCheck.position, 0.5f, _groundLayer);
            if (!_grounded)
            {
                // temporary solution
                if (transform.eulerAngles == new Vector3(0, 180, 0))
                {
                    transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);
                }

            }

        }

        if (!_spotRange.IsTouchingLayers(_playerLayer))
        {
            _state = State.Attention;
        }
    }

    private void Attack()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        _animator.SetTrigger("Attack");


    }

    // This is triggered by animation event
    public void DealDamage()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(_damagePoint.position, _attackRange, _playerLayer);

        if (hitPlayer)
        {
            hitPlayer.GetComponent<ICharacter>().TakeDamage(_damage);

            // pushing the player - needs improvement
            if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                hitPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(-_pushForce, 0f));
            }
            else
            {
                hitPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(_pushForce, 0f));
            }
        }
    }

    private void StayAlert()
    {
        StartCoroutine(ListeningForPlayer());
    }

    private IEnumerator ListeningForPlayer()
    {
        _attentionMark.SetActive(true);
        _rigidbody2D.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(_attentionTime);
        _attentionMark.SetActive(false);
        _state = State.Patrolling;
    }
}