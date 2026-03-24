using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    public Transform CameraTransform;

    void Start()
    {
        
    }

    void Update()
    {
        _Transform.LookAt(CameraTransform);
    }
}
