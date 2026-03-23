using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Transform TargetTransform;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        _Transform.LookAt(TargetTransform);
    }
}
