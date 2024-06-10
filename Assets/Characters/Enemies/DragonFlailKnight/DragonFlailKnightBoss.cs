using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonFlailKnightBoss : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public DetectionZone agroZone;
    public DetectionZone dashZone;

    public GameObject Explosive;
    public Transform SpawnLocation;
    public Transform SpawnLocation1;
    public Transform SpawnLocation2;
    public Transform SpawnLocation3;

    public bool walk = true;
    public float number;
    public int _slamCounter = 5;

    TouchingDirections touchingDirections;

    float nextAttackTime = 0f;
    public float nextDashTime = 0f;

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

    public bool _HasTargetToDash = false;

    public bool HasTargetToDash
    {
        get { return _HasTargetToDash; }
        private set
        {
            _HasTargetToDash = value;

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
        if (touchingDirections.IsGrounded && touchingDirections.IsWall || cliffDetectionZone.detectedColliders.Count == 0)
        {
            FlipDirection();
        }
    }

    void Update ()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        HasTargetToDash = dashZone.detectedColliders.Count > 0;
        HasAgro = agroZone.detectedColliders.Count > 0;

        if (_hasTarget == true && Time.time >= nextAttackTime)
        {
            _animator.SetTrigger("hasTarget");
            nextAttackTime = Time.time + 2f;
        }

        if (_hasAgro == true && _hasTarget == false)
        {
            _animator.SetBool("_agressiveWalking", true);

        }
        else if (_hasAgro == true && _hasTarget == true)
        {
            _animator.SetBool("_agressiveWalking", false);

        }

    }

    private void FlipDirection()
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

    public void RandomDash()
    {
        int number = Random.Range(0, 6);
        _animator.SetInteger("Dash", number);
    }

    public void RandomAttack()
    {
        int number = Random.Range(0, 10);
        _animator.SetInteger("Attack", number);
    }

    public void Spawn()
    {
        Instantiate(Explosive, SpawnLocation.position, Quaternion.identity);
        Instantiate(Explosive, SpawnLocation1.position, Quaternion.identity);
        Instantiate(Explosive, SpawnLocation2.position, Quaternion.identity);
        Instantiate(Explosive, SpawnLocation3.position, Quaternion.identity);
    }


   
}
