using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour
{
    public float baseSpeed;
    public float speed;

    private Rigidbody2D rb;
    private Vector2 movement;

    public Transform groundDetection;
    public Transform wallCheck;
    public Transform playerCheck;
    public Transform playerCheck2;

    public Animator animator;

    public float baseDistance;
    public float distance;
    public float agroDistance;

    public float attackRange;

    public Transform player;

    bool isFlipped = false;

    void Start()
    {
        distance = baseDistance;
        speed = baseSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        ///print("distToPlayer:" + distToPlayer);


        RaycastHit2D playerInfo2 = Physics2D.Raycast(playerCheck2.position, Vector2.right, agroDistance, LayerMask.GetMask("Player"));
        RaycastHit2D playerInfo = Physics2D.Raycast(playerCheck.position, Vector2.left, agroDistance, LayerMask.GetMask("Player"));
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, Vector2.left, distance, LayerMask.GetMask("Wall"));
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, LayerMask.GetMask("Ground"));
        if (groundInfo.collider == false || wallInfo.collider == true)
        {
            transform.Rotate(0f, 180f, 0f);
        }
        ///if (playerInfo.collider == true || playerInfo2.collider == true && wallInfo.collider == false) 
        else if (playerInfo.collider == true || playerInfo2.collider == true && wallInfo.collider == false && distToPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            speed = 1;
            distance = 2;
            agroDistance = 4;
            //transform.localScale = new Vector2(1, 1);
            LookAtPlayer();
        }
       else
        {
            ///animator.SetBool("Move", true);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            speed = baseSpeed;
            distance = baseDistance;
            agroDistance = 3;
            //transform.localScale = new Vector2(1, 1);
        }

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
            transform.Translate(0, 0, 0);
            animator.SetTrigger("Attack");
            }
       

        void LookAtPlayer()
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }

        }

    }


}

