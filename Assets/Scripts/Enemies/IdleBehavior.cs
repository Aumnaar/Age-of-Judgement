using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    private Rigidbody2D rb;

    Transform GC;

    public LookAtPlayer lookAtPlayer;
    public Transform target;
    public float chaseDistance;


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
        if (Physics2D.Raycast(GC.position, Vector2.down, 4) == false)
        {
            return;
        }

        if (Vector2.Distance(target.position, rb.position) <= chaseDistance)
        {
            animator.SetBool("Moving", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }

}
