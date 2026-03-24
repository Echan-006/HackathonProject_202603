using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot0_C : MonoBehaviour
{
    [SerializeField] ShotPool _ShotPool;

    [SerializeField] Transform _Transform;

    public bool isCoroutine = false;
    const float TIME = 0.1f;
    const int CREATE_NUM = 2;

    const float ROTATION_Y_MIN = -40f;
    const float ROTATION_Y_MAX = 60f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
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

    public void CreateStart()
    {
        isCoroutine = true;
        StartCoroutine(ShotCoroutine());
    }

    public void CreateEnd()
    {
        isCoroutine = false;
    }

    IEnumerator ShotCoroutine()
    {
        while (isCoroutine)
        {
            yield return new WaitForSeconds(TIME);

            for (int i = 0; i < CREATE_NUM; i++)
            {
                //_ShotPool.Shot0Get(_Transform.position, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(ROTATION_Y_MIN, ROTATION_Y_MAX), 0));
                GameObject Obj = _ShotPool.ShotObjPool.Get();
                ShotParent Obj_ShotParent = Obj.GetComponent<ShotParent>();
                Transform ObjTransform = Obj_ShotParent.Objects[0].transform;
                ObjTransform.position = _Transform.position;
                ObjTransform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(ROTATION_Y_MIN, ROTATION_Y_MAX), 0);
                //Obj.GetComponent<ShotObjManager>().Scripts[0].enabled = true;
                Obj_ShotParent.Objects[0].SetActive(true);
            }
        }
    }
}
