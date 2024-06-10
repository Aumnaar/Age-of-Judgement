using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlailBossDash : StateMachineBehaviour
{
    DragonFlailKnightBoss _dragonFlailKnightBoss;
    private float _dashingPower = 3f;
    [SerializeField] private Rigidbody2D rb;
    public Transform player;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        _dragonFlailKnightBoss = animator.GetComponent<DragonFlailKnightBoss>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.position.x > player.position.x)
        {
            rb.velocity = new Vector2(animator.transform.localScale.x + -_dashingPower, 0f);
            //rb.AddForce(-animator.transform.right * _dashingPower, ForceMode2D.Force);
        }
        else if (animator.transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(_dragonFlailKnightBoss.walkSpeed + _dashingPower, rb.velocity.y);
            //rb.AddForce(animator.transform.right * _dashingPower, ForceMode2D.Force);
        }
        else if (animator.transform.position.x == player.position.x)
        {
            int number = 0;
            animator.SetInteger("Dash", number);
            _dragonFlailKnightBoss.walkSpeed = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int number = 0;
        animator.SetInteger("Dash", number);
        _dragonFlailKnightBoss.walkSpeed = 0;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
