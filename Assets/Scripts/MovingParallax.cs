using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingParallax : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public int _number;
    float backgroundImageWidth;
    private float startPose;
    
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundImageWidth = sprite.texture.width / sprite.pixelsPerUnit;
        startPose = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float MoveX = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(MoveX, 0);
        if (Mathf.Abs(transform.position.x) - backgroundImageWidth >_number)
        {
            transform.position = new Vector3(startPose, transform.position.y);
        }
        
    }
}
