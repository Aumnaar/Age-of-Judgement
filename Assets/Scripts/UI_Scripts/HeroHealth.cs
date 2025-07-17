using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    public bool manaShield;

    public GameObject player;
    public Slider healthSlider;
    public Slider secondSlider;
    public Slider manaSlider;
    public float lerpSpeed = 0.05f;

    public float maxHealth = 80;
    public float currentHealth;

    public float maxMana = 80;
    public float currentMana;

    public float maxStagger = 100;
    public float currentStagger;

    public bool isAlive = true;
    public bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public Animator animator;

    public GameObject shieldOfWill;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStagger = 0;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L)) 
        //    {
        //        TakeDamage(20);
        //    }

        //healthBar.fillAmount = Mathf.Clamp(currentHealth/maxHealth, 0, 1);

        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;

        }

        if (Input.GetKeyDown(KeyCode.K) && currentMana > 20 && currentHealth < maxHealth)
        {
            TakeHeal(20);
            MinusMana(20);
        }

        if (healthSlider.value != secondSlider.value)
        {
            secondSlider.value = Mathf.Lerp(secondSlider.value, currentHealth, lerpSpeed);
        }

        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

    }

   
  //--------------------------------------------------

    public void TakeDamage(int damage,  int stagger)
    {
        if (isAlive && !isInvincible && !manaShield)
        {
            currentHealth -= damage;
            currentStagger += stagger;
            SetHealth(currentHealth);
            TakeStagger();
            isInvincible = true;

        }
        else if (isAlive && !isInvincible && manaShield)
        {
            ManaShield(damage);
        }
     
    }

    public void TakeHeal(int heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += heal;


            SetHealth(currentHealth);
        }
    }

    public void TakeStagger()
    {
        if (currentStagger >= 100)
        {
            currentStagger = 0;
            animator.SetTrigger("onHit");
        }
    }

    //-----------------------------------------------

    public void MinusMana (int mana)
    {
        if (currentMana > 0)
        {
            currentMana -= mana;
            SetMana(currentMana);
        }
    }

    public void PlusMana(int mana)
    {
        if (currentMana < maxMana)
        {
            currentMana += mana;
            SetMana(currentMana);
        }
    }

    //---------------------------------------------

    void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    void SetHealth(float health)
    {
        healthSlider.value = health;


    }

    //----------------------------------------------

    void SetMaxMana(int mana)
    {
        manaSlider.maxValue = mana;
        manaSlider.value = mana;
    }

    void SetMana(float mana)
    {
        manaSlider.value = mana;


    }
    //----------------------------------------------
    private void ManaShield(int damage)
    {
        if (currentMana >= damage)
        {
            shieldOfWill.SetActive(true);
            currentMana -= damage;
            SetMana(currentMana);
            isInvincible = true;

        }
        else if (currentMana < damage)
        {
            currentHealth -= damage;
            SetHealth(currentHealth);
            isInvincible = true;
            animator.SetTrigger("onHit");
            shieldOfWill.SetActive(false);
            Debug.Log("damage");

        }
    }

    //----------------------------------------------

    private void Die()
    {

        SceneManager.LoadScene(0);

    }
}
