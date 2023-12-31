using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressuveBehavior : StateMachineBehaviour
{

    public float speed;
    public float attackRange = 3f;

    public Transform player;

    private Rigidbody2D rb;

    public LookAtPlayer lookAtPlayer;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lookAtPlayer = animator.GetComponent<LookAtPlayer>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rb = animator.GetComponent<Rigidbody2D>();

        lookAtPlayer.LookToPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            
            animator.SetTrigger("meleeAttack");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
   }

}
