using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWalk : StateMachineBehaviour
{
    SwordKnight _swordKnight;
    TouchingDirections touchingDirections;
    [SerializeField] private Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _swordKnight = animator.GetComponent<SwordKnight>();
        touchingDirections = animator.GetComponent<TouchingDirections>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (touchingDirections.IsGrounded && touchingDirections.IsWall || _swordKnight.cliffDetectionZone.detectedColliders.Count == 0)
        {
            _swordKnight.FlipDirection();
        }
        rb.velocity = new Vector2(_swordKnight.walkSpeed * _swordKnight._directionVector.x, rb.velocity.y);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
