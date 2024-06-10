using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public Transform launchPoint;

    public DetectionZone attackZone;

    float nextAttackTime = 0f;


    public bool _hasAgro = false;

    public bool HasAgro
    {
        get { return _hasAgro; }
        private set
        {
            _hasAgro = value;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        HasAgro = attackZone.detectedColliders.Count > 0;

        if (transform.position.x > player.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x < player.position.x)
        {

           transform.localScale = new Vector3(-1, 1, 1);
        }


        if (_hasAgro == true && Time.time >= nextAttackTime)
        {
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.transform.position, projectilePrefab.transform.rotation /*Quaternion.identity*/);
            Vector3 origScale = projectile.transform.localScale;

            projectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);

            nextAttackTime = Time.time + 2f;
        }
    }
}

////Throwing//////
    //var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            //_projectile.GetComponent<Rigidbody2D>().velocity = launchSpeed * launchPoint.up;