using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{

    [SerializeField] private Deeds deeds;

    [Header ("Movement Parameters")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Animator anim;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] public bool _canWalk;
    public bool _isAttacking = false;
    public bool _walking;


    [Header("Additional Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;


    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Layers")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Ground Checks")]
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    [Header("Attacks")]


    private string currentState;
    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string JUMP = "Jump";
    const string HA1 = "HA1";
    const string HA2 = "HA2";

    //[Header("Sounds")]

    //NOTES
    // attackDelay = animator.GetCurrentAnimatorStateInfo(0).length - wait until current animation be finished

    void Awake()
    {
        _walking = false;
        _canWalk = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
    }


    void Start()
    {
        jumpCounter = extraJumps;
    }

 
    void Update()
    {

        ////MOVEMENT//////

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");



        //rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y); \\cant jump freely without it, though



        //if (isGrounded())
        //{


            if (isGrounded() && horizontalInput != 0 && _canWalk && !CombatManager.instance._isAttacking)
            {
                //rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);
                //anim.SetBool("Walk", true);
                //anim.Play("Walk");
                ChangeAnimationState(WALK);
            }
            else
            {
                //rb.velocity = new Vector2(0, rb.velocity.y);
                //anim.SetBool("Walk", false);
                //anim.Play("Idle");
                ChangeAnimationState(IDLE);
            }


        //}





        //if (isGrounded())
        //{

        //    anim.SetBool("Walk", horizontalInput != 0);

        //}

        if (horizontalInput > 0)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        ////////
        ///
        /// 

        ///

        ////JUMPING

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            anim.SetTrigger("Jump");
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isGrounded())
        {
            Jump2();
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0)
        {
          rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
        //////
    }

    void Ignore()
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);
    }

    private void Jump()
    {
        if (!isGrounded() && coyoteCounter < 0 && jumpCounter <= 0) return;


        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);

        }
    }
    
    private void Jump2()
    {        
        if (!isGrounded() && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
            
        }
        else if (!isGrounded() && coyoteCounter <= 0 && jumpCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
            jumpCounter--;
           
        }
    }

    public bool isGrounded()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _groundLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
    }

    void ChangeAnimationState(string NewState)
    {
        if (currentState == NewState) return;

        anim.Play(NewState);

        currentState = NewState;

    }


}
