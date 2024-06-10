using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;

    public int maxHealth = 80;
    public int currenthealth;
    public Rigidbody2D rb;

    public LayerMask player;
    public Animator animator;

    public bool isAlive = true;

    public bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    float nextAttackTime = 0f;

  

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

       // if (!isAlive)
       // {
           // Invoke("Die", 1f);
      //  }//

    }

    public void TakeDamage(int damage, Vector2 deliveredKnockback)
    {
        if (isAlive && !isInvincible) 
        {
            damagableHit.Invoke(damage, deliveredKnockback);
            currenthealth -= damage;
            isInvincible = true;

        }

        if (currenthealth <= 0)
        {
            //animator.SetTrigger("Death");
            isAlive = false;
            Die();

        }
    }

    public void OnHit (int damage, Vector2 deliveredKnockback)
    {
        animator.SetTrigger("onHit");
        rb.velocity = new Vector2(deliveredKnockback.x, rb.velocity.y);
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
