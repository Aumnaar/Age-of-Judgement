using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    Collider2D col;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
