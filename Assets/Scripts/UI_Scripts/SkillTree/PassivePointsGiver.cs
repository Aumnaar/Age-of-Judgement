using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassivePointsGiver : MonoBehaviour
{
    public SkillTreeManager manager;
    //public Collider2D col;
    //public GameObject usedUnblocker;
    //public GameObject unblocker;
    public bool isUsed;

    void Start()
    {
        isUsed = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterMenu player = collision.GetComponent<CharacterMenu>();

        if (Input.GetKey(KeyCode.E) && !isUsed && collision.gameObject.CompareTag("Player"))
        {
                //unblocker.SetActive(false);
                //usedUnblocker.SetActive(true);
                isUsed = true;
            manager.UpdateAbilityPoints(+1);
            Destroy(gameObject);
                
            
        }

    }
}
