using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCamera : MonoBehaviour
{
    [SerializeField] Transform _Transform;

    private float posX = 0;
    const int POS_X_SPEED = 25;
    const float POS_X_MAX = 3.5f;

    const int POS_Y = 1;
    const int POS_Z = 3;

    void Start()
    {
        
    }

    void Update()
    {
        posX += POS_X_SPEED * -Input.GetAxis("Mouse X") * Time.deltaTime;
        posX = Mathf.Clamp(posX, -POS_X_MAX, POS_X_MAX);
        _Transform.localPosition = new Vector3(posX, POS_Y, POS_Z);
    }

    private void FixedUpdate()
    {
        //_Transform.localPosition = new Vector3(posX, POS_Y, POS_Z);
    }
}
