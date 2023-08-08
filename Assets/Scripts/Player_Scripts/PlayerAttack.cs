using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        Destructable destructable = collision.GetComponent<Destructable>();

        if (enemy != null)
        {
            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            destructable.GetComponent<Destructable>().Shaking();
            destructable.GetComponent<Destructable>().Ruin();

        }
    }
}
