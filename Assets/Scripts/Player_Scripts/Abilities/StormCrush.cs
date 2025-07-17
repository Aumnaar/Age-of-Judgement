using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class StormCrush : MonoBehaviour
{
    public PlayerCombat pc;
    public HeroHealth herohealth;
    public float knockbackForce = 4f;
    public int bonusDamage;
    public int stun;


    private void Start()
    {
        herohealth = GetComponentInParent<HeroHealth>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        EnemyBoss enemyBoss = collision.GetComponent<EnemyBoss>();

        if (enemy != null)
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;

            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeStun(stun);
            enemy.GetComponent<Enemy>().TakeStagger(pc.baseAttackDamage * bonusDamage);
            enemy.GetComponent<Enemy>().TakeDamage(pc.baseAttackDamage * bonusDamage);
            enemy.GetComponent<Enemy>().OnHit(knockback);

        }
    }
}
