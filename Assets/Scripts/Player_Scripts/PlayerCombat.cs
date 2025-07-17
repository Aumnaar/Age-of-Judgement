using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public CharacterMenu characterMenu;
    public HeroHealth herohealth;
    public PlayerMov playerMov;

    private int veryBaseDamage;
    public int baseAttackDamage;
    public int baseManaRegen;
    public int knockback = 2;

    public int stormCrushManaCost;
    public int lightningSpearManaCost;
   
    public LayerMask enemies;
    public GameObject player;

    public bool Syphoning;

    public float waiting = 1f;
    public Animator animator;

    float nextAttackTime = 0f;

    private void Start()
    {
        veryBaseDamage = baseAttackDamage;
        animator = GetComponent<Animator>();
        //baseAttackDamage = 10;
        Number();
    }

    private void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.R) && characterMenu.StormCrushActive && playerMov.isGrounded() && herohealth.currentMana >= stormCrushManaCost)
        {
            animator.Play("Crush");
            herohealth.MinusMana(stormCrushManaCost);
        }

        if (Input.GetKeyDown(KeyCode.V) && characterMenu.LightningSpearActive && playerMov.isGrounded())
        {
            Debug.Log("Lightning and thunder!");
            herohealth.MinusMana(lightningSpearManaCost);
        }
    }

    private void Number()
    {
        //Mathf.Round(baseAttackDamage);
        float Mana = (Mathf.Round(baseAttackDamage) / 100) * 30;

        //baseManaRegen = Convert.ToInt32(baseAttackDamage / 100) * 30;
        baseManaRegen = (int)Mana;
    }

    public void Syphon()
    {
        if (Syphoning)
        {
            var Damage = baseAttackDamage / 2;
            baseAttackDamage = Damage;

            baseManaRegen = baseManaRegen * 2;
        }
        else if (!Syphoning)
        {
            baseAttackDamage = veryBaseDamage;
            Number();
        }
    }

}