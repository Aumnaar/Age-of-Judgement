using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public bool AgroMechanicsEnabled = true;

    TouchingDirections touchingDirections;
    public PlayerCombat pc;
    public Animator animator;
    public Transform player;
    private Renderer rend;
    public Rigidbody2D rb;

    [Header("Detection zones")]
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public DetectionZone agroZone;
    public DetectionZone detectionZone;

    public float xVelocity; //for movement in blendtree

    [Header("Base parameters")]
    public int maxHealth = 80;
    public int currenthealth;
    public float walkSpeed = 3f; /*actual walkSpeed in animationBehaviour*/
    public float decayTime = 5f;

    [Header("Stun and stagger")]
    public int Stun = 100;
    public int currentStun;
    public float stunTime;
    public int Stagger = 100;
    public int currentStagger;
    public int staggerResistance; 
    public float staggerTime;
    public float knockbackResistance; /*divider*/

    [Header("Primary bool")]
    public bool isAlive = true;
    public bool isStunned = false;
    public bool isMovable = true;

    [Header("Secondary bool")]
    public bool isUnstunnable = false;
    public bool isUnstaggerable = false;
    public bool isInvincible = false;
    public bool isHit = false;

    [Header("Time since")]
    private float timeSinceHit = 0;
    private float timeSinceHittoStun = 0;
    private float timeSinceStun = 0;
    private float timeSinceStagger = 0;
    private float timeSinceHittoStagger = 0;

    [Header("Invincibility time")]
    public float invincibilityTime = 0.25f;
    public float unstannableTime = 0.25f;
    public float unstaggerableTime = 0.25f;

    /// walkin
    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    public Vector2 _directionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {

            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    _directionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    _directionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }


    }

    /// Enemy detection

    public bool _hasAgro = false;

    public bool HasAgro
    {
        get { return _hasAgro; }
        private set
        {
            _hasAgro = value;

        }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;

        }
    }


    void Start()
    {
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxHealth;
        currentStun = 0;
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

        if (isHit)
        {

           if (timeSinceHittoStun > 8)
          {
                isHit = false;
                currentStun = 0;
                timeSinceHittoStun = 0;
            }

            timeSinceHittoStun += Time.deltaTime;

        }

        if (isHit)
        {

            if (timeSinceHittoStagger > 3)
            {
               
                currentStagger = 0;
                timeSinceHittoStagger = 0;
            }

            timeSinceHittoStagger += Time.deltaTime;

        }

        if (isUnstunnable)
        {
            if (timeSinceStun > unstannableTime)
            {
                isUnstunnable = false;
                timeSinceStun = 0;
            }

            timeSinceStun += Time.deltaTime;
        }

        if (isUnstaggerable)
        {
            if (timeSinceStagger > unstaggerableTime)
            {
                isUnstaggerable = false;
                timeSinceStagger = 0;
            }

            timeSinceStagger += Time.deltaTime;
        }


        ////////////////////////

        if (AgroMechanicsEnabled)
        {
            HasAgro = detectionZone.detectedColliders.Count > 0;

            if (_hasAgro == true)
            {
                animator.SetBool("hasAgro", true);

            }

            HasTarget = agroZone.detectedColliders.Count > 0;

            if (_hasTarget == true && _hasAgro == true)
            {
                animator.SetFloat("xVelocity", 1);
                animator.SetBool("hasTarget", true);
            }
            else if (_hasTarget == false && _hasAgro == true)
            {
                //animator.SetFloat("xVelocity", 0);
                animator.SetBool("hasTarget", false);
            }
            else if (_hasAgro == false)
            {
                animator.SetFloat("xVelocity", 0);
                animator.SetBool("hasAgro", false);
            }

        }
    }

    public void TakeStun(int stun)
    {

        if (currentStun < Stun && !isInvincible && !isStunned)
        {
            Debug.Log("StunChargin" + currentStun);
            currentStun += stun;
        }

        if (!isInvincible && currentStun >= Stun && !isStunned && !isUnstunnable)
        {
            Debug.Log("stun");
            currentStun = 0;
            //animator.Play("Stun");
            animator.SetTrigger("Stun");
            StartCoroutine(StunTime(stunTime));
        }
    }

    public void TakeStagger (int damage)
    {
        var finalStagger = damage - staggerResistance;

        if (currentStagger < Stagger && !isInvincible && !isStunned)
        {
            currentStagger += finalStagger;
        }

        if (!isInvincible && !isStunned && !isUnstaggerable && currentStagger >= Stagger)
        {
        StartCoroutine(StaggerTime(staggerTime));
            currentStagger = 0;
        }

    }

    public void TakeDamage(int damage)
    {
        

        if (isAlive && !isInvincible) 
        {
            currenthealth -= damage;
            isInvincible = true;
            isHit = true;

        }
       

        if (currenthealth <= 0)
        {
            //animator.SetTrigger("Death");
            isAlive = false;
            Die();

        }
    }

    public void OnHit (Vector2 knockback)
    {
        if (isMovable && !isStunned)
        {

            Vector2 finalKnockback = knockback / knockbackResistance;
            animator.SetTrigger("onHit");
            rb.velocity = new Vector2(finalKnockback.x, rb.velocity.y);

        }
        else if (isMovable && isStunned)
        {
            rb.velocity = new Vector2(knockback.x, rb.velocity.y);
        }
    }

     IEnumerator StunTime(float stunTime)
    {
        isStunned = true;
        isUnstunnable = true;
        rend.material.color = Color.cyan;
        yield return new WaitForSeconds(stunTime);
        rend.material.color = Color.white;
        animator.SetTrigger("noStun");
        isStunned = false;
      
    }

    IEnumerator StaggerTime(float staggerTime)
    {
        isUnstaggerable = true;
        animator.SetTrigger("Stagger");
        yield return new WaitForSeconds(staggerTime);
        animator.SetTrigger("noStun");



    }

    private IEnumerator DecayTime(float decayTime)
    {
        yield return new WaitForSeconds(decayTime);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
        Destroy(this);
        


    }

    private void Die()
    {
        if (!isAlive)
        {
            animator.SetTrigger("Destruction");
            Debug.Log("Enemy died");
            StartCoroutine(DecayTime(decayTime));
        }
    }

    public void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Direction not set");
        }
    }


}
