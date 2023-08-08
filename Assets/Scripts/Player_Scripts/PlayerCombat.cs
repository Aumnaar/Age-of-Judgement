using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 10;
   
    public LayerMask enemies;
    public GameObject player;
    public bool isAlive = true;
    public bool isInvincible = false;

    //public HealthBar healthBar;

    public int maxHealth = 80;
    public int currentHealth;

    public float waiting = 1f;
    public Animator animator;

    float nextAttackTime = 0f;

    void Start ()
    {
        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //if (Time.time >= nextAttackTime)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        ///Attack();
        //        animator.SetTrigger("Attack");
        //        nextAttackTime = Time.time + 1f / attackRate;
        //    }
        //}
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Enemy enemy = collision.GetComponent<Enemy>();

    //   if (enemy != null)
    //        {
    //        Debug.Log("We hit" + enemy.name);
    //        enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
           
    //    }
    //}

    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;

    //    healthBar.SetHealth(currentHealth);

    //    if (currentHealth <= 0)
    //    {
    //           Die();
    //    }
    //}

    private void Die()
    {
      
        SceneManager.LoadScene(0);
       
    }
}
