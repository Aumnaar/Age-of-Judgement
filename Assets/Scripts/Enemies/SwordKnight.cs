 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKnight : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public DetectionZone agroZone;

    public bool walk = true;
    public float number;

    TouchingDirections touchingDirections;

    float nextAttackTime = 0f;

    public float walkSpeed = 3f;
    Rigidbody2D rb;

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

    public bool _hasAgro = false;

    public bool HasAgro { get { return _hasAgro; }
        private set
        {
            _hasAgro = value;

        }
    }

    public bool _hasTarget = false;

    public bool HasTarget { get { return _hasTarget; } private set
        {
            _hasTarget = value;
            
        }
    }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        //rb.velocity = new Vector2(walkSpeed * _directionVector.x, rb.velocity.y);

        //if (touchingDirections.IsGrounded && touchingDirections.IsWall || cliffDetectionZone.detectedColliders.Count == 0)
        //{
        //    FlipDirection();
        //}


    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (_hasTarget == true)
        {
            _animator.SetBool("_agressiveWalking", false);
            _animator.SetTrigger("hasTarget");
          
            walk = false;
        }

        if (walk == true)
        {
            _animator.SetBool("_isWalking", true);
        }
        else
        {
            _animator.SetBool("_isWalking", false);
        }

        HasAgro = agroZone.detectedColliders.Count > 0;

        if (_hasAgro == true && _hasTarget == false)
        {
            _animator.SetBool("_agressiveWalking", true);
            walk = false;
        }
        else if (_hasAgro == true && _hasTarget == true)
        {
            _animator.SetBool("_agressiveWalking", false);
            walk = false;
         
        }
        else if (_hasAgro == false && _hasTarget == false)
        {
            walk = true;
            _animator.SetBool("_isWalking", true);
            _animator.SetBool("_agressiveWalking", false);

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
