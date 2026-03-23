using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Rigidbody _Rigidbody;
    [SerializeField] Animator _Animator;

    AnimatorStateInfo CurrentStateInfo;
    AnimatorStateInfo NextStateInfo;

    private int idleHash = Animator.StringToHash("Idle");
    private int walkHash = Animator.StringToHash("Walk");
    private int jumpHash = Animator.StringToHash("Jump");
    private int jumpEndHash = Animator.StringToHash("JumpEnd");

    const float IDLE_CROSS = 0.25f;
    const float WALK_CROSS = 0.15f;
    const float JUMP_CROSS = 0.15f;

    const int JUMP_FORCE = 5;

    [SerializeField] Transform TargetTransform;

    private float moveX;
    private float moveZ;

    const float SPEED = 5.5f;

    public bool canJump = true;
    public bool canPreJump = false;
    public bool preJump = false;
    public bool fixedVelocityY = false;

    public bool isGround = true;

    public bool isFalling { get; private set; } = false;
    private float posLastY;
    const float ERROR_FALL = 0.01f;

    [SerializeField] Transform ModelTransform;

    const float ROTATE_SPEED = 7.5f;
    Quaternion RotationLast;

    void Start()
    {
        posLastY = _Transform.position.y;
        RotationLast = ModelTransform.rotation;
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        _Transform.LookAt(TargetTransform);
        _Transform.rotation = Quaternion.Euler(0, _Transform.eulerAngles.y, 0);

        CurrentStateInfo = _Animator.GetCurrentAnimatorStateInfo(0);
        NextStateInfo = _Animator.GetNextAnimatorStateInfo(0);

        if (isGround && canJump)
        {
            if (moveX == 0 && moveZ == 0)
            {
                if (CurrentStateInfo.shortNameHash != idleHash && NextStateInfo.shortNameHash != idleHash)
                {
                    _Animator.CrossFade(idleHash, IDLE_CROSS);
                }
            }
            else
            {
                if (CurrentStateInfo.shortNameHash != walkHash && NextStateInfo.shortNameHash != walkHash)
                {
                    _Animator.CrossFade(walkHash, WALK_CROSS);
                }
            }
        }

        if (canJump)
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

        if (moveX == 0 && moveZ == 0)
        {
            ModelTransform.rotation = RotationLast;
        }
        else
        {
            float angle = Mathf.Atan2(moveX, moveZ) * Mathf.Rad2Deg;
            ModelTransform.localRotation = Quaternion.Euler(0, angle, 0);
            Quaternion RotationBase = ModelTransform.rotation;
            ModelTransform.localRotation = Quaternion.Euler(0, angle, 0);
            Quaternion RotationTarget = ModelTransform.rotation;
            ModelTransform.rotation = Quaternion.RotateTowards(RotationBase, RotationTarget, ROTATE_SPEED * Time.deltaTime);

            RotationLast = ModelTransform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (fixedVelocityY)
        {
            _Rigidbody.velocity = new Vector3(_Rigidbody.velocity.x, 0, _Rigidbody.velocity.z);
        }

        _Rigidbody.MovePosition(_Transform.position + SPEED * Time.fixedDeltaTime * (_Transform.right * moveX + _Transform.forward * moveZ));

        float posDeltaY = _Rigidbody.position.y - posLastY;
        isFalling = (posDeltaY < 0 && posDeltaY >= ERROR_FALL);
        posLastY = _Rigidbody.position.y;
    }

    public void Jump()
    {
        fixedVelocityY = false;
        _Rigidbody.AddForce(JUMP_FORCE * Vector3.up, ForceMode.Impulse);
    }

    public void JumpEnd()
    {
        canJump = true;
    }

    public void PreJump()
    {
        canPreJump = true;
    }
}