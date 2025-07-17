using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deeds : MonoBehaviour
{
    [SerializeField] private PlayerMov mov;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 vector;
    private float timer;
    public GameObject teleportPoint;


    public GameObject weather;
    public GameObject _lightClouds;
    public GameObject _darkClouds;
    public GameObject _rain;
    public bool _isClear = true;


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

    public void Teleport()
    {
        transform.position = new Vector2(teleportPoint.transform.position.x, teleportPoint.transform.position.x);
    }
 
    public void Weather()
    {
        StartCoroutine(WeatherDelete());
    }

    private IEnumerator WeatherDelete()
    {
        if (_isClear)
        {
            _isClear = false;
            weather.SetActive(true);
            yield return new WaitForSeconds(1);
            _lightClouds.SetActive(false);
            _darkClouds.SetActive(true);
            _rain.SetActive(true);
            yield return new WaitForSeconds(1);
            weather.SetActive(false);
        }
        else
        {
            _isClear = true;
            weather.SetActive(true);
            yield return new WaitForSeconds(1);
            _lightClouds.SetActive(true);
            _rain.SetActive(false);
            _darkClouds.SetActive(false);
            yield return new WaitForSeconds(1);
            weather.SetActive(false);
        }
    }

}
