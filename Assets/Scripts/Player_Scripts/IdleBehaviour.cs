using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField] private PlayerMov _playerMov;
    [SerializeField] private Rigidbody2D rb;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerMov = animator.GetComponent<PlayerMov>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
     
        

        if (CombatManager.instance._isAttacking && _playerMov.isGrounded())
        {
            CombatManager.instance.anim.Play("StormHaloAttack_One");
        }
        else if (CombatManager.instance._isAttacking && !_playerMov.isGrounded())
        {
            CombatManager.instance.anim.Play("Aerial1");
        }


        if (_playerMov.isGrounded() && horizontalInput != 0 && !CombatManager.instance._isAttacking)
        {
            animator.SetBool("Walk", true);

        }
        else
        {
            animator.SetBool("Walk", false);

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
