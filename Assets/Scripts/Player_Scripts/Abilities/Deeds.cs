using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeds : MonoBehaviour
{
    [SerializeField] private PlayerMov mov;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 vector;
    private string currentState;
    private float delay;
    private float timer;

    
    const string IDLE = "Idle";
    const string JUMP = "Jump";

   // NOTES
  //  rb.constraints = RigidbodyConstraints2D.FreezePosition ///
 //   rb.constraints = RigidbodyConstraints2D.FreezeRotation ///


    void Awake()
    {
      
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
    }

    void Update()
    {
     

      
    }

    void ChangeAnimationState(string NewState)
    {
        if (currentState == NewState) return;

        anim.Play(NewState);

        currentState = NewState;
        
    }

    void Ground()
    {
        if (mov.isGrounded()) 
        {
            
            

        }
    }        

}
