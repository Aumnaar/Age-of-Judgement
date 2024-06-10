using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        EnemyBoss enemyBoss = collision.GetComponent<EnemyBoss>();
        //Destructable destructable = collision.GetComponent<Destructable>();

        if (enemy != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage, deliveredKnockback);
          

        }
        else if (enemyBoss != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            Debug.Log("We hit" + enemyBoss.name);
            enemyBoss.GetComponent<EnemyBoss>().TakeDamage(attackDamage, deliveredKnockback);
        }
    }
}
