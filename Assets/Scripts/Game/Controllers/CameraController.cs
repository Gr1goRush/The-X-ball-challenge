using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Vector3 ballOffset;

    public void SetPosition(Vector3 ballPosition)
    {
        ballPosition.x = 0;
        ballPosition.y = 0;
        transform.position = ballPosition + ballOffset;
    }
}
