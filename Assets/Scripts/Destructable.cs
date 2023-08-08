using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public Enemy enemy;
    public Animator animator;

    public bool isShaking = false;
    public float shakeAmount;
    private Vector2 startPos;

  

    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
    }

    void Update()
    {

        if (isShaking)
        {
            transform.position = startPos + UnityEngine.Random.insideUnitCircle * shakeAmount;
            Invoke("ResetShaking", 0.1f);
        }

    }

 public void Shaking()
    {
        isShaking = true;
    }


    void ResetShaking()
    {
        isShaking = false;
        transform.position = startPos;
    }

    public void Ruin()
    {
        if (enemy.currenthealth <= 70)
        {
            animator.SetTrigger("Ruin");
        }

        if (enemy.currenthealth <= 50)
        {
            animator.SetTrigger("Ruin");
        }
      
        if (enemy.currenthealth <= 0)
        {
            animator.SetTrigger("Ruin");
           
        }
    }

  
}
