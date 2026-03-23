using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Rigidbody _Rigidbody;

    [SerializeField] Transform TargetTransform;

    const float SPEED = 100;

    float moveX;
    float moveZ;

    void Start()
    {
        
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        _Transform.LookAt(TargetTransform);
        _Transform.position += Time.fixedDeltaTime * (_Transform.right * moveX + _Transform.forward * moveZ);

        //_Transform.position += velocity * Time.deltaTime * new Vector3(moveY, 0, moveX);
    }

    private void FixedUpdate()
    {
        //_Transform.LookAt(TargetTransform);
        //_Rigidbody.MovePosition(_Rigidbody.position + Time.fixedDeltaTime * (_Transform.right * moveX + _Transform.forward * moveZ));
    }
}
