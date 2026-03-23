using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    [SerializeField] PlayerMove _PlayerMove;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Jump()
    {
        _PlayerMove.Jump();
    }

    public void JumpEnd()
    {
        _PlayerMove.JumpEnd();
    }

    public void PreJump()
    {
        _PlayerMove.PreJump();
    }
}
