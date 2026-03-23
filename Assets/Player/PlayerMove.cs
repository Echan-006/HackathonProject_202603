using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Rigidbody _Rigidbody;
    [SerializeField] Animator _Animator;

    private int idleHash = Animator.StringToHash("Idle");
    private int walkHash = Animator.StringToHash("Walk");
    private int jumpHash = Animator.StringToHash("Jump");
    private int jumpEndHash = Animator.StringToHash("JumpEnd");

    const float JUMP_CROSS = 0.15f;

    const int JUMP_FORCE = 5;

    [SerializeField] Transform TargetTransform;

    private float moveX;
    private float moveZ;

    const float SPEED = 5.5f;

    private bool canJump = true;
    private bool canPreJump = false;
    private bool preJump = false;
    private bool fixedVelocityY = false;

    private bool isGround = true;

    void Start()
    {
        
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        _Transform.LookAt(TargetTransform);
        _Transform.rotation = Quaternion.Euler(0, _Transform.eulerAngles.y, 0);

        if (canJump && isGround)
        {
            if(Input.GetKeyDown(KeyCode.Space) || preJump)
            {
                _Animator.CrossFade(jumpHash, JUMP_CROSS);
                canJump = false;
                canPreJump = false;
                preJump = false;
                fixedVelocityY = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canPreJump)
            {
                preJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (fixedVelocityY)
        {
            _Rigidbody.velocity = new Vector3(_Rigidbody.velocity.x, 0, _Rigidbody.velocity.z);
        }

        _Rigidbody.MovePosition(_Transform.position + SPEED * Time.fixedDeltaTime * (_Transform.right * moveX + _Transform.forward * moveZ));
    }

    public void Jump()
    {
        fixedVelocityY = false;
        _Rigidbody.AddForce(JUMP_FORCE * Vector3.up, ForceMode.Impulse);
    }

    public void JumpEnd()
    {
        canJump = true;
        canPreJump = false;
        preJump = false;
    }

    public void PreJump()
    {
        canPreJump = true;
        preJump = false;
    }
}