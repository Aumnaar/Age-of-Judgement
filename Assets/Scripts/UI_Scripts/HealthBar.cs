using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void UpdateHealthBar(float currentValue)
    {
        slider.value = currentValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
