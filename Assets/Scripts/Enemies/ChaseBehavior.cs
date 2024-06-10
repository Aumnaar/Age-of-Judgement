using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{
    public float speed;
    public float attackRange = 1f;

    public Transform target;

    Transform GC;

    private Rigidbody2D rb;

    public LookAtPlayer lookAtPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lookAtPlayer = animator.GetComponent<LookAtPlayer>();
        rb = animator.GetComponent<Rigidbody2D>();
        GC = animator.GetComponent<LookAtPlayer>().GC;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Vector2 target = new Vector2(target.position.x, rb.position.y);
        /// Vector2 newPos = Vector2.MoveTowards(target.position.x, animator.position.y);
        //rb.MovePosition(newPos);
        lookAtPlayer.LookToPlayer();

        Vector2 newPos = new Vector2(target.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, newPos, speed * Time.deltaTime);

        if (Physics2D.Raycast(GC.position, Vector2.down, 4) == false)
        {
            animator.SetBool("Moving", false);
        }

        if (Vector2.Distance(target.position, rb.position) < attackRange)
         {
           lookAtPlayer.LookToPlayer();
           animator.SetBool("Attack", true);
          }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
