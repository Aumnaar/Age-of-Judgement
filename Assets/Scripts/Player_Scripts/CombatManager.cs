using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private PlayerMov playermov;
    public static CombatManager instance;
    public Animator anim;
    public bool _isAttacking = false;

 
 

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            _isAttacking = true;
            
         

        }


     }
}
