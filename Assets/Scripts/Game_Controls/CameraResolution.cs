using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int GameOrthographicSize = 6;
    [SerializeField] private float GameAspect = 0.46f;

    private float defaultWidth;

    private void Start()
    {
       //_cameraMain = Camera.main;

       defaultWidth = GameOrthographicSize * GameAspect;
       _camera.orthographicSize = defaultWidth / _camera.aspect;

    }

    private void Update()
    {

    }

}
