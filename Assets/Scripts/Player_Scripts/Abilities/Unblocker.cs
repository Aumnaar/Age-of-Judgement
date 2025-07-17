using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unblocker : MonoBehaviour
{
    public SkillSlot skillSlot;
    public Collider2D col;
    public GameObject usedUnblocker;
    public GameObject unblocker;
    public bool isUsed;

    // Start is called before the first frame update
    void Start()
    {
        isUsed = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterMenu player = collision.GetComponent<CharacterMenu>();

        if (Input.GetKey(KeyCode.E) && !isUsed && collision.gameObject.CompareTag("Player"))
        {
            if (!player.StormCrushActive)
            {
                player.GetComponent<CharacterMenu>().StormCrushActive = true;
                unblocker.SetActive(false);
                usedUnblocker.SetActive(true);
                isUsed = true;
                skillSlot.TryUpgradeActiveSkill();
            }
        }

    }
}
