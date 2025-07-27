using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerCombat pc;
    public HeroHealth herohealth;
    public int stun;
    public float knockbackForce = 4f;
    public Transform player;
    public int bonusDamage;
    //public int attackDamage = 10;
    //public int mana;
    //public Vector2 knockback = Vector2.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            enemy.GetComponent<Enemy>().TakeStagger(pc.baseAttackDamage);
            enemy.GetComponent<Enemy>().TakeDamage(pc.baseAttackDamage * bonusDamage);
            enemy.GetComponent<Enemy>().OnHit(knockback);

            herohealth.PlusMana(pc.baseManaRegen);


        }
    }
}
