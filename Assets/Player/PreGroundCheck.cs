using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGroundCheck : MonoBehaviour
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
        _PlayerMove.canPreJump = true;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ground"))
    //    {
    //        _PlayerMove.canPreJump = true;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        _PlayerMove.canPreJump = false;
    }
}
