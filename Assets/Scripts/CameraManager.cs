using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;

    public CinemachineVirtualCamera _FirstCam;
    public CinemachineVirtualCamera _SecondCam;

    public CinemachineVirtualCamera _startCam;
    public CinemachineVirtualCamera _currentCam;

    // Start is called before the first frame update
    void Start()
    {
        _currentCam = _startCam;
       for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == _currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
                {
                cameras[i].Priority = 10;
            }
        }
        
    }

   public void SwitchCamera(CinemachineVirtualCamera newCam)
    {
        _currentCam = newCam;
        _currentCam.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != _currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}
