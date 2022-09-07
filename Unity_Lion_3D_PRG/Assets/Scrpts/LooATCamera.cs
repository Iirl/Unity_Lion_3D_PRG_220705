using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooATCamera : MonoBehaviour
{
    Transform mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }
    private void Update()
    {
        if (mainCamera != null) transform.LookAt(mainCamera);
    }
}
