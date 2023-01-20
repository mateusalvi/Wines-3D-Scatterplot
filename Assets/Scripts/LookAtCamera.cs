using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera _main_camera;
    private void Start()
    {
        _main_camera = Camera.allCameras[0];
    }
    void Update()
    {
        transform.LookAt(_main_camera.transform);
    }
}
