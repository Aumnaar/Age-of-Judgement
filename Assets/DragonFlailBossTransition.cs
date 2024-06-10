using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlailBossTransition : StateMachineBehaviour
{
    DragonFlailKnightBoss _dragonFlailKnightBoss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _dragonFlailKnightBoss = animator.GetComponent<DragonFlailKnightBoss>();
        _dragonFlailKnightBoss.walk = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_dragonFlailKnightBoss.walkSpeed != 0)
        {
            _dragonFlailKnightBoss.walkSpeed = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _dragonFlailKnightBoss.walkSpeed = 1;

        if (_dragonFlailKnightBoss._hasTarget == true && _dragonFlailKnightBoss._hasAgro && _dragonFlailKnightBoss._slamCounter >= 0)
        {
            int number = Random.Range(0, 10);
            animator.SetInteger("Attack", number);
            _dragonFlailKnightBoss._slamCounter -= 1;
        }

        if (_dragonFlailKnightBoss._slamCounter == 0)
        {
            animator.SetTrigger("Slam");
            _dragonFlailKnightBoss._slamCounter = 5;
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
