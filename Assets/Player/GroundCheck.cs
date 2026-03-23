using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerMove _PlayerMove;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _PlayerMove.isGround = true;
        _PlayerMove.canJump = true;
        _PlayerMove.canPreJump = true;
        _PlayerMove.preJump = false;
        _PlayerMove.fixedVelocityY = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        _PlayerMove.isGround = true;
    //        _PlayerMove.canJump = true;
    //        _PlayerMove.canPreJump = true;
    //        _PlayerMove.preJump = false;
    //        _PlayerMove.fixedVelocityY = false;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        _PlayerMove.isGround = false;
    }
}
