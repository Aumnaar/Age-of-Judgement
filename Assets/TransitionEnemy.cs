using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEnemy : StateMachineBehaviour
{
    SwordKnight _swordKnight;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _swordKnight = animator.GetComponent<SwordKnight>();
        _swordKnight.walk = false;
        //_swordKnight.number = 1;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_swordKnight.walkSpeed != 0)
        {
            _swordKnight.walkSpeed = 0;
        }

        if (_swordKnight._hasTarget == false)
        {
            _swordKnight.walk = true;
        }

        if (_swordKnight._hasTarget == true && _swordKnight._hasAgro)
        {
            float number = Random.Range(1, 6 + 2);
            animator.SetFloat("attackNumber", number);
        }



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _swordKnight.walkSpeed = 1;

        if (_swordKnight._hasTarget == true && _swordKnight._hasAgro)
        {
            int number = Random.Range(1, 2);
            animator.SetInteger("attackNumber", number);
        }

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
