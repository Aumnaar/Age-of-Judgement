using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnterTriggerComponent : MonoBehaviour
{
    public CameraManager _cameraManager;
    [SerializeField] private string _tag;

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        
            Debug.Log("Exit");
            _cameraManager.SwitchCamera(_cameraManager._SecondCam);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        Debug.Log("Enter");
        _cameraManager.SwitchCamera(_cameraManager._startCam);

    }

}
