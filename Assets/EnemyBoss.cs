using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;

    public int maxHealth = 200;
    public int currenthealth;
    public Rigidbody2D rb;

    public LayerMask player;
    public Animator animator;

    public bool isAlive = true;

    public bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    float nextAttackTime = 0f;

    public HealthBar healthbar;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxHealth;
        //healthbar = GetComponent<HealthBar>();
        healthbar.UpdateHealthBar(currenthealth);
    }

    void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;

        }

    }

    public void TakeDamage(int damage, Vector2 deliveredKnockback)
    {
        if (isAlive && !isInvincible)
        {
            damagableHit.Invoke(damage, deliveredKnockback);
            currenthealth -= damage;
            healthbar.UpdateHealthBar(currenthealth);
            isInvincible = true;

        }

        if (currenthealth <= 0)
        {
            animator.SetTrigger("Death");
            isAlive = false;
           

        }
    }

    public void OnHit(int damage, Vector2 deliveredKnockback)
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
