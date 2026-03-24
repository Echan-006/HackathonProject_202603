using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Rigidbody _Rigidbody;
    [SerializeField] Animator _Animator;

    public int life { get; private set; } = 10;
    public int lifeMax { get; private set; } = 10;

    AnimatorStateInfo CurrentStateInfo;
    AnimatorStateInfo NextStateInfo;

    private int idleHash = Animator.StringToHash("Idle");
    private int walkHash = Animator.StringToHash("Walk");
    private int jumpHash = Animator.StringToHash("Jump");
    private int jumpEndHash = Animator.StringToHash("JumpEnd");
    private int deathHash = Animator.StringToHash("Death");

    const float IDLE_CROSS = 0.25f;
    const float WALK_CROSS = 0.15f;
    const float JUMP_CROSS = 0.15f;
    const float DEATH_CROSS = 0.2f;

    const int JUMP_FORCE = 5;

    [SerializeField] Transform TargetTransform;

    private float moveX;
    private float moveZ;

    const float SPEED = 4.5f;

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

    [SerializeField] GameObject NovaObj;
    public ObjectPool<GameObject> NovaPool;

    [SerializeField] GameObject DamageObj;
    [SerializeField] Transform DamageParent;
    public ObjectPool<GameObject> DamagePool;
    Quaternion DamageRotation = Quaternion.Euler(-90, 0, 0);
    const int DAMAGE_NUM = 3;

    private bool isAttacked = false;
    private float fixedOperationTime = 0;
    const float FIXED_OPERATION_TIME_MAX = 0.25f;
    public float damagePerformanceTime { get; private set; } = 0;
    const float DAMAGE_PERFORMANCE_TIME_MAX = 0.5f;

    private bool canDamage = true;
    const float DAMAGE_WAIT = 0.75f;

    void Start()
    {
        life = lifeMax;
        isAttacked = false;
        _Rigidbody.useGravity = true;
        canDamage = true;

        posLastY = _Transform.position.y;
        RotationLast = ModelTransform.rotation;
        NovaPool = new ObjectPool<GameObject>
            (
                createFunc: () =>
                {
                    GameObject Obj = Instantiate(NovaObj);
                    Obj.GetComponent<JumpNova>()._PlayerMove = this;
                    return Obj;
                },
                actionOnGet: Obj =>
                {
                    Obj.SetActive(true);
                    Obj.transform.position = _Transform.position;
                },
                actionOnRelease: Obj => Obj.SetActive(false),
                actionOnDestroy: Obj => Destroy(Obj),
                collectionCheck: true,
                defaultCapacity: 3,
                maxSize: 10
            );
        DamagePool = new ObjectPool<GameObject>
            (
                createFunc: () =>
                {
                    GameObject Obj = Instantiate(DamageObj);
                    return Obj;
                },
                actionOnGet: Obj =>
                {
                    Obj.SetActive(true);
                    Transform ObjTransform = Obj.transform;
                    ObjTransform.parent = DamageParent;
                    ObjTransform.localPosition = Vector3.zero;
                    ObjTransform.localRotation = DamageRotation;
                    ObjTransform.parent = null;
                },
                actionOnRelease: Obj => Obj.SetActive(false),
                actionOnDestroy: Obj => Destroy(Obj),
                collectionCheck: true,
                defaultCapacity: 3,
                maxSize: 12
            );
    }

    void Update()
    {
        CurrentStateInfo = _Animator.GetCurrentAnimatorStateInfo(0);
        NextStateInfo = _Animator.GetNextAnimatorStateInfo(0);

        if (life <= 0)
        {
            _Rigidbody.useGravity = true;
            fixedOperationTime = 0;
            damagePerformanceTime = 0;
            _Animator.speed = 1;
            if (CurrentStateInfo.shortNameHash != deathHash && NextStateInfo.shortNameHash != deathHash && isGround)
            {
                _Animator.CrossFade(deathHash, DEATH_CROSS);
            }
            return;
        }

        if (fixedOperationTime > 0)
        {
            fixedOperationTime -= Time.deltaTime / FIXED_OPERATION_TIME_MAX;
        }
        else
        {
            isAttacked = false;
            _Animator.speed = 1;
        }
        fixedOperationTime = Mathf.Clamp01(fixedOperationTime);

        if (damagePerformanceTime > 0)
        {
            damagePerformanceTime -= Time.deltaTime / DAMAGE_PERFORMANCE_TIME_MAX;
        }
        damagePerformanceTime = Mathf.Clamp01(damagePerformanceTime);

        if (isAttacked) return;

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        _Transform.LookAt(TargetTransform);
        _Transform.rotation = Quaternion.Euler(0, _Transform.eulerAngles.y, 0);

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
        if (life <= 0) return;

        _Rigidbody.useGravity = !isAttacked;

        if (isAttacked) return;

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
        if (!isGround)
        {
            NovaPool.Get();
        }

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

    public void Damage()
    {
        if (life <= 0) return;
        if (!canDamage) return;

        canDamage = false;
        StartCoroutine(DamageCoroutine());

        life--;
        for (int i = 0; i < DAMAGE_NUM; i++)
        {
            DamagePool.Get();
        }
        if (!isAttacked)
        {
            _Rigidbody.velocity = Vector3.zero;
            _Rigidbody.useGravity = false;
            fixedOperationTime = 1;
            damagePerformanceTime = 1;
            _Animator.speed = 0;
        }
        isAttacked = true;
    }

    IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(DAMAGE_WAIT);

        canDamage = true;
    }
}