using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public KeyCode empurao;
    public Rigidbody2D inimigo;
    public float power = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(empurao))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemie in hitEnemies)
        {
            Vector3 direction = inimigo.gameObject.transform.position - this.transform.position;
            inimigo.gameObject.GetComponent<Player_1>().StartCooldown();
            direction.z = 0;
            direction.Normalize();
            inimigo.AddForceAtPosition(direction * power, transform.position, ForceMode2D.Force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
