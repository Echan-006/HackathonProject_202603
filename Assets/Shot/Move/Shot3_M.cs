using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot3_M : MonoBehaviour
{
    [SerializeField] Transform _Transform;

    const int ROTATE_SPEED = 40;

    void Start()
    {
        
    }

    void Update()
    {
        _Transform.Rotate(0, ROTATE_SPEED * Time.deltaTime, 0);    
    }
}
