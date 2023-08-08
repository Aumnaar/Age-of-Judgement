using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public ParticleSystem _dust;
  
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _secondJumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Animator _animator;
    private SpriteRenderer sprite;

   
    [SerializeField] private int playerJumps;
    public int tempPlayerJumps;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    [SerializeField] private float fallMulti;
    [SerializeField] private float lowJumpMulti;

    /// For respawn after fallin
    //[SerializeField] private float timer = 15;
    //[SerializeField] private Vector3 place;


      ///private Vector2 _direction;
      ///

      private void Awake()
    {
        //place = transform.position;
        _animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


   // public void Update()
    //{
     //   float x = Input.GetAxis("Horizontal");
     ///   float y = Input.GetAxis("Vertical");
     //   Vector2 dir = new Vector2(x, y);

     //   Walk(dir);
     //   Jump();

    //}

    public void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        Jump();

        ///Timer for teleporting after falling
        ///timer -= Time.deltaTime;
      //  if (timer <= 0 && isGrounded())
       // {
       //    place = transform.position;
       //     timer = 15;
       // }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
        }
    }

    public void FixedUpdate()
    {
        if (rb.velocity.y < _maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, _maxFallSpeed);
        }
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * _speed, rb.velocity.y);

        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetBool("_isWalking", true);
        }
        else
        {
            _animator.SetBool("_isWalking", false);
        }

        if (dir.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
           /// sprite.flipX = true;
        }
        else
        {
            transform.localScale = Vector3.one;
            //sprite.flipX = false;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            tempPlayerJumps = playerJumps;
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            rb.velocity = Vector2.up * _jumpForce;
            // rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            ///tempPlayerJumps--;
            ///CreateDust();
        }
        else if (Input.GetKeyDown(KeyCode.W) && tempPlayerJumps > 0 & !isGrounded())
        {
            rb.velocity = Vector2.up * _secondJumpForce;
            ///rb.velocity += Vector2.up * _secondJumpForce;
            //rb.AddForce(Vector2.up * _secondJumpForce, ForceMode2D.Impulse);
           tempPlayerJumps--;
       }

       }
    

    private bool isGrounded()
    {
        var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadius, Vector2.down, 0, _groundLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
    }

    //public void FlyUp()
    //{
    //    transform.position = place;
    //}
}
    ///void CreateDust()
    //{
   //     _dust.Play();
   // }