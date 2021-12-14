using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] CinemachineImpulseSource aimImpulseSource;


    private void Awake()
    {
        Instance = this;
    }

    public void ShakeAimCamera()
    {
        aimImpulseSource.GenerateImpulse();
    }
}
