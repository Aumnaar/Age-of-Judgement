using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public int attackStagger;
    public int mana;
    public bool isSelfKnockback = false;
    public float knockbackForce = 4f;

    public Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        HeroHealth herohealth = collision.GetComponent<HeroHealth>();
        //Destructable destructable = collision.GetComponent<Destructable>();

        if (herohealth != null)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;

            Debug.Log("We hit" + herohealth.name);
            herohealth.GetComponent<HeroHealth>().TakeDamage(attackDamage, attackStagger);

            if (isSelfKnockback)
            {
                enemy.OnHit(knockback *4);
            }
        }
    }
}
