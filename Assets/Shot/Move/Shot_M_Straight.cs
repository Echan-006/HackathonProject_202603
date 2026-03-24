using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_M_Straight : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] private float speed;
    [SerializeField] private float speedFirst;
    [SerializeField] private float speedLimit;
    [SerializeField] private float acceleration;

    void OnEnable()
    {
        speed = speedFirst;
    }

    void Update()
    {
        if (acceleration > 0)
        {
            if (speed < speedLimit)
            {
                speed += acceleration * Time.deltaTime;
            }
            if (speed >= speedLimit)
            {
                speed = speedLimit;
            }
        }
        else
        {
            if (speed > speedLimit)
            {
                speed += acceleration * Time.deltaTime;
            }
            if (speed <= speedLimit)
            {
                speed = speedLimit;
            }
        }

        _Transform.position += speed * _Transform.forward * Time.deltaTime;
    }
}
