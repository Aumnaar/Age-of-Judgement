using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 80;
    public int currenthealth;
    private int npcdamage = 5;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    public LayerMask player;
    public Animator animator;

    public bool isAlive = true;

    public bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    float nextAttackTime = 0f;

    void Start()
    {
        currenthealth = maxHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible =false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;

        }

        if (!isAlive)
        {
            Invoke("Die", 1f);
        }

        //if (Time.time >= nextAttackTime)
        //{
        //    animator.SetTrigger("Attack");
        //    ///Attack();
        //    nextAttackTime = Time.time + 1f / attackRate;
        

        //}
    }

    public void TakeDamage(int damage)
    {
        if (isAlive && !isInvincible) 
        {
            currenthealth -= damage;
            isInvincible = true;
        }

        if (currenthealth <= 0)
        {
            isAlive = false;
            
        }
    }

    //public void TakeDamage(int damage)
    //{
    //    currenthealth -= damage;


    //    if (currenthealth <= 0)
    //    {
    //        Die();
    //    }
    //}

    //public void Attack()
    //{
    //    Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, player);
    //    foreach (Collider2D player in hitPlayer)
    //    {
    //        Debug.Log("We hit" + player.name);
    //        player.GetComponent<PlayerCombat>().TakeDamage(npcdamage);
    //    }

    //}

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Die()
    {
        if (!isAlive)
        {
            Debug.Log("Enemy died");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
            Destroy(this);
        }
    }
}
