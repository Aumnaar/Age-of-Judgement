using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.5f;
    public float wallDistance = 0.5f;

    Rigidbody2D rb;
    CapsuleCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _IsGrounded;

    private Vector2 wallCheck => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded {  get { return _IsGrounded; } private set { _IsGrounded = value; } }

    [SerializeField]
    private bool _IsWall;

    public bool IsWall { get { return _IsWall; } private set { _IsWall = value; } }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsWall = touchingCol.Cast(wallCheck, castFilter, wallHits, wallDistance) > 0;
    }
}
