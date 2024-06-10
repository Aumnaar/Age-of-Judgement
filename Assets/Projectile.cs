using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 speed = new Vector2(3f, 0);

    private Transform player;

    private Vector2 target;

    private Rigidbody2D rb;

    private float gravity = 1;
    public float damageRadius;

    public float travelDistance;
    private float xStartPose;

    private bool isGravityOn;
    private bool hasHitGround;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public Transform damagePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        //rb.velocity = transform.right * speed;
        isGravityOn = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void Update ()
    {

        //if (!hasHitGround)
        //{
        //    if (isGravityOn)
        //    {
        //        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //    }
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        rb.velocity = new Vector2(speed.x * transform.localScale.x, speed.y);

        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

                if (damageHit)
            {
                Destroy(gameObject);
            }


                if (groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                Destroy(gameObject);
            }


            if (Mathf.Abs(xStartPose - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }

    
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }

    //public void FireProjectile(float speed, float travelDistance, float damage)
    //{
    //    this.speed = speed;
    //    this.travelDistance = travelDistance;

    //}

}
