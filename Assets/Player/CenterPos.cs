using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPos : MonoBehaviour
{
    [SerializeField] Transform _Transform;

    [SerializeField] Transform PlayerTransform;
    [SerializeField] Transform EnemyTransform;

    void Start()
    {
        
    }

    void Update()
    {
        _Transform.position = (PlayerTransform.position + EnemyTransform.position) / 2;
    }
}
