using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject _CurrentOneWayPlatform;
    [SerializeField] private CapsuleCollider2D playerCollider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            {
            Physics2D.IgnoreLayerCollision(3, 9, true);
                StartCoroutine(EnableCollision());
            }   
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            _CurrentOneWayPlatform = collision.gameObject;
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    _CurrentOneWayPlatform = null;
    //}

    private IEnumerator EnableCollision()
    {
        //CapsuleCollider2D platformCollider = _CurrentOneWayPlatform.GetComponent<CapsuleCollider2D>();
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(3, 9, false);
       
    }
}
