using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnterTriggerCameraComponent : MonoBehaviour
{
    public CameraManager _cameraManager;
    [SerializeField] private string _tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enter");

            _cameraManager.SwitchCamera(_cameraManager._SecondCam);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        //if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exit");

            _cameraManager.SwitchCamera(_cameraManager._startCam);
        }
    }

}
