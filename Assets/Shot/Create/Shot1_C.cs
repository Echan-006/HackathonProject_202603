using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Shot1_C : MonoBehaviour
{
    [SerializeField] ShotPool _ShotPool;

    [SerializeField] Transform _Transform;
    [SerializeField] Transform PlayerTransform;

    Quaternion PreRotation;
    Vector3 Right;
    Vector3 Up;

    private bool isCoroutine;

    const float TIME_SHORT = 0.05f;
    const float TIME_LONG = 3.5f;

    private int[] distance = { 30, 24, 18, 12 };
    const int HALF_CIRCLE_NUM = 10;

    const int DEG_TO_RAD = 180;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!isCoroutine)
            {
                CreateStart();
            }
            else
            {
                CreateEnd();
            }
        }
    }

    void CreateStart()
    {
        //float deltaX = PlayerTransform.position.x - _Transform.position.x;
        //float deltaZ = PlayerTransform.position.z - _Transform.position.z;

        //PreRotation = _Transform.rotation;
        //_Transform.rotation = Quaternion.Euler(0, Mathf.Atan2(deltaZ, deltaX) * Mathf.Rad2Deg, 0);
        //Right = _Transform.right;
        //Up = _Transform.up;
        //_Transform.rotation = PreRotation;

        isCoroutine = true;
        StartCoroutine(ShotCoroutine());
    }

    void CreateEnd()
    {
        isCoroutine = false;
    }

    IEnumerator ShotCoroutine()
    {
        while (isCoroutine)
        {
            Vector3 Delta = (PlayerTransform.position - _Transform.position).normalized;
            Right = Vector3.Cross(Vector3.up, Delta).normalized;

            for (int i = 0; i < distance.Length; i++)
            {
                //float deltaX = PlayerTransform.position.x - _Transform.position.x;
                //float deltaZ = PlayerTransform.position.z - _Transform.position.z;

                //PreRotation = _Transform.rotation;
                //_Transform.rotation = Quaternion.Euler(0, Mathf.Atan2(deltaZ, deltaX) * Mathf.Rad2Deg - 90, 0);
                //Right = _Transform.right;
                //Up = _Transform.up;
                //_Transform.rotation = PreRotation;

                for (int j = 0; j < HALF_CIRCLE_NUM + 1; j++)
                {
                    float angle = DEG_TO_RAD * ((float)j / HALF_CIRCLE_NUM);

                    GameObject Obj = _ShotPool.ShotObjPool.Get();
                    GameObject Obj_1 = Obj.GetComponent<ShotParent>().Objects[1];
                    Obj_1.SetActive(true);
                    Shot1_M _Shot1_M = Obj_1.GetComponent<Shot1_M>();
                    _Shot1_M.FirstPos = _Transform.position;
                    _Shot1_M.TargetPos = _Transform.position + Right * distance[i] * Mathf.Cos(angle * Mathf.Deg2Rad) + Vector3.up * distance[i] * Mathf.Sin(angle * Mathf.Deg2Rad);
                    _Shot1_M.PlayerTransform = PlayerTransform;
                    Obj_1.transform.position = _Shot1_M.FirstPos;

                    yield return new WaitForSeconds(TIME_SHORT);
                }
            }

            yield return new WaitForSeconds(TIME_LONG);
        }
    }
}
