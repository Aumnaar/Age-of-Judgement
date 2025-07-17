using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWalking : StateMachineBehaviour
{
    Enemy enemy;
    [SerializeField] private Rigidbody2D rb;
    public Transform player;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy._hasTarget && enemy._hasAgro)
        {
            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, enemy.walkSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (animator.transform.position.x > player.position.x)
            {
                animator.transform.localScale = new Vector3(1, 1, 1);

            }
            else if (animator.transform.position.x < player.position.x)
            {

                animator.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

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
