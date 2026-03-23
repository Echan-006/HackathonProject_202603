using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_M_Straight : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] private float velocity = 6;

    void Start()
    {
        
    }

    void Update()
    {
        _Transform.position += velocity * _Transform.forward * Time.deltaTime;
    }
}
