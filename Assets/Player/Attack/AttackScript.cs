using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] Transform _Transform;

    public Transform Enemy;
    public AttackPool _AttackPool;

    [SerializeField] Transform ModelTransform;

    private float speed = 0;
    const int SPEED_MAX = 35;
    const int SPEED_MIN = 3;

    private float attackValue = 0;
    const float ATTACK_TIME = 1.5f;

    private bool canAttack = false;

    const int ROTATION_MAX = 1000;

    Vector3 PosFirst;

    Vector3 SizeMax = new Vector3(1, 1, 1);
    [SerializeField] private float sizeValue = 0;
    const float SIZE_TIME = 0.35f;

    void OnEnable()
    {
        speed = 0;
        attackValue = 0;
        canAttack = false;
        sizeValue = 0;
    }

    void Update()
    {
        if (sizeValue < 1)
        {
            sizeValue += Time.deltaTime / SIZE_TIME;
        }
        sizeValue = Mathf.Clamp01(sizeValue);
        _Transform.localScale = Vector3.Lerp(Vector3.zero, SizeMax, Mathf.Sqrt(sizeValue));
        _Transform.LookAt(Enemy);

        if (canAttack)
        {
            if (attackValue < 1)
            {
                attackValue += Time.deltaTime / ATTACK_TIME;
            }
            attackValue = Mathf.Clamp01(attackValue);

            //speed = Mathf.Lerp(SPEED_MAX, SPEED_MIN, attackValue);

            //_Transform.position += speed * _Transform.forward * Time.deltaTime;
            _Transform.position = Vector3.Slerp(PosFirst, Enemy.position, Mathf.Sqrt(attackValue));
        }
        else
        {
            speed = 0;
            attackValue = 0;
        }
    }

    private void LateUpdate()
    {
        ModelTransform.rotation = Quaternion.Euler(0, 0, 0);
       
        //if (!canAttack)
        //{
        //    ModelTransform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else
        //{
        //    ModelTransform.rotation = Quaternion.Euler(0, 0, ModelTransform.eulerAngles.z + Mathf.Lerp(0, ROTATION_MAX, attackValue));
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");

        if (!canAttack)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                canAttack = true;
                PosFirst = _Transform.position;
                //GameObject Obj = _AttackEffectPool.EffectPool.Get();
                //Transform ObjTransform = Obj.transform;
                //ObjTransform.parent = _Transform;
                //ObjTransform.localPosition = Vector3.zero;
                //ObjTransform.localRotation = Quaternion.Euler(0, 0, 180);
                //ObjTransform.parent = null;
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy.gameObject.GetComponent<Enemy>().Damage();
                _AttackPool.AttackObjPool.Release(this.gameObject);
            }
        }
    }
}
