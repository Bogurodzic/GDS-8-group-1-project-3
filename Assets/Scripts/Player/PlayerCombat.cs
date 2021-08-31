using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _damagePoint;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private PlayerMovementController _movementController;

    [Header("Controls")]
    [SerializeField] private KeyCode _attackButton;

    [Header("Player Combat Stats")]
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private int _attackDamage = 5;
    [SerializeField] private float _pushForce = 500f;

    // Update is called once per frame
    void Update()
    {
        if (!Inventory.IsInventoryOpened && Input.GetKeyDown(_attackButton) && !_movementController.doubleJumpActivated)
        {
            Attack();
        }
    }

    void Attack()
    {
        _animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_damagePoint.position, _attackRange, _enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Target hit: " + enemy.gameObject.name);

            enemy.GetComponent<ICharacter>().TakeDamage(_attackDamage);

            if (enemy.GetComponent<EnemyCombatMelee>())
            {
                enemy.GetComponent<EnemyCombatMelee>().PushBack(_pushForce, 0);
            }
            else if (enemy.GetComponent<EnemyCombatRanged>())
            {
                enemy.GetComponent<EnemyCombatRanged>().PushBack(_pushForce, 0);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_damagePoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_damagePoint.position, _attackRange);
    }
}
