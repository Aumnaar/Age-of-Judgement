using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : StateMachineBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerMov _playerMov;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        _playerMov = animator.GetComponent<PlayerMov>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");



        rb.velocity = new Vector2(horizontalInput * _speed, rb.velocity.y);

        if (horizontalInput == 0)
        {
            animator.SetBool("Walk", false);
        }



        if (CombatManager.instance._isAttacking && _playerMov.isGrounded())
        {
            CombatManager.instance.anim.Play("HA1");
        }
        else if (CombatManager.instance._isAttacking && !_playerMov.isGrounded())
        {
            CombatManager.instance.anim.Play("Aerial1");
        }
        else if (Input.GetKeyDown(KeyCode.R) && _playerMov.isGrounded())
        {
            CombatManager.instance.anim.Play("Crush");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CombatManager.instance._isAttacking = false;
    }

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
