using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDamageCollider : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Transform PlayerTransform;

    void Start()
    {
        
    }

    void Update()
    {
        _Transform.position = PlayerTransform.position;
    }
}
