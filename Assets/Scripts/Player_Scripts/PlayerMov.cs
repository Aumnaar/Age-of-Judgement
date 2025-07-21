using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{

    [SerializeField] private Deeds deeds;
    [SerializeField] private CharacterMenu characterMenu;

    [Header ("Movement Parameters")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Animator anim;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] public bool _canWalk;
    public bool _isAttacking = false;
    [SerializeField] int glideSpeed = 10;
  


    [Header("SlopeManagment")]
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Transform playerBottom;
    private RaycastHit2D Hit2D;

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

    [Header("Trails")]
    //public ParticleSystem footprint;
    //public ParticleSystem footprint1;
    public Animator _footprints;

    [Header ("Menu")]
    public bool menuOpened = false;
    public bool menuPowersOpened = false;



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

        SlopeMathod();

        ////MOVEMENT//////

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        var speed = rb.velocity.x;

        if (speed != 0)
        {
            _footprints.SetBool("Print", true);
        }
        else if (speed == 0)
        {
            _footprints.SetBool("Print", false);
        }

        //rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);

        ////Special Abilities////

        if (Input.GetKeyDown(KeyCode.T))
        {
            deeds.Teleport();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            deeds.Weather();
        }

        if (Input.GetKeyDown(KeyCode.M) && !menuOpened)
        {
            characterMenu.EnableMenu();
            menuOpened = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            characterMenu.DisableMenu();
            menuOpened = false;
            
        }


        //if (isGrounded() && horizontalInput != 0 && _canWalk && !CombatManager.instance._isAttacking)
        //{
        //    //rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);
        //    //anim.SetBool("Walk", true);
        //    //anim.Play("Walk");
        //    ChangeAnimationState(WALK);
        //}
        //else
        //{
        //    //rb.velocity = new Vector2(0, rb.velocity.y);
        //    //anim.SetBool("Walk", false);
        //    //anim.Play("Idle");
        //    ChangeAnimationState(IDLE);
        //}


        //if (horizontalInput > 0)
        //    transform.localScale = Vector3.one;
        //else if (horizontalInput < 0)
        //    transform.localScale = new Vector3(-1, 1, 1);


        //if (isGrounded() && horizontalInput == 0)
        //{
        //    rb.velocity = Vector2.zero;
        //    rb.isKinematic = true;
        //    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //}
        //else
        //{
        //    rb.isKinematic = false;
        //    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //}



        ////JUMPING//////////////////

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
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.W) && !isGrounded())
        {
            Jump2();
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0)
        {
          rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2);
        }
       
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > 5 && Input.GetKey(KeyCode.Space)) 
        { rb.velocity = Vector2.ClampMagnitude(rb.velocity, 2); }

    }
    
    ////////////////////////////////////////////////
   
    

    void Ignore()
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);
    }

    private void SlopeMathod()
    {
        Hit2D= Physics2D.Raycast(raycastOrigin.position, -Vector2.up, -100f, _groundLayer);
        if (Hit2D != false)
        {
            Vector2 temp = playerBottom.position;
            temp.y = Hit2D.point.y;
            playerBottom.position = temp;
        
        }
    }

    private void Jump()
    {
        if (!isGrounded() && coyoteCounter < 0 && jumpCounter <= 0) return;


        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, _jumpPower);
            //rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

        }
    }
    
    private void Jump2()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (!isGrounded() && coyoteCounter > 0)
        {
            rb.velocity = new Vector2(horizontalInput * _speed, _jumpPower);
            
        }
        else if (!isGrounded() && coyoteCounter <= 0 && jumpCounter > 0)
        {
            rb.velocity = new Vector2(horizontalInput * _speed, _jumpPower);
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

    //public void CreateFootprint()
    //{
    //    footprint.Play();
    //}

    //public void CreateFootprint1()
    //{
    //    footprint1.Play();
    //}

    void ChangeAnimationState(string NewState)
    {
        if (currentState == NewState) return;

        anim.Play(NewState);

        currentState = NewState;

    }


}
