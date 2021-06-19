using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _damagePoint;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private KeyCode _attackButton;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private int _attackDamage = 5;
    [SerializeField] private float _pushForce = 500f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_attackButton))
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
            enemy.GetComponent<ICharacter>().TakeDamage(_attackDamage);
            if (transform.eulerAngles == new Vector3(0, 180, 0))
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-_pushForce, 0f));
            }
            else
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(_pushForce, 0f));
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
