using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotPool : MonoBehaviour
{
    public ObjectPool<GameObject> ShotObjPool;
    [SerializeField] GameObject ShotObj;
    MonoBehaviour[] MoveScripts;

    [SerializeField] private int actionNum;  //使用中のオブジェクト数
    [SerializeField] private int poolNum;  //プールが持ってる全オブジェクト数

    [SerializeField] Shot0_C _Shot0_C;

    void Start()
    {
        ShotObjPool = new ObjectPool<GameObject>
            (
                createFunc: () =>
                {
                    GameObject Obj = Instantiate(ShotObj);
                    Obj.GetComponent<ShotObjManager>()._ShotPool = this;
                    poolNum++;
                    return Obj;
                },
                actionOnGet: Obj =>
                {
                    ResetScript(Obj);
                    actionNum++;
                    Obj.SetActive(true);
                },
                actionOnRelease: Obj =>
                {
                    ResetScript(Obj);
                    actionNum--;
                    Obj.SetActive(false);
                },
                actionOnDestroy: Obj =>
                {
                    ResetScript(Obj);
                    poolNum--;
                    actionNum--;
                    Destroy(Obj);
                },
                collectionCheck: true,
                defaultCapacity: 100,
                maxSize: 500
            );
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (!_Shot0_C.isCoroutine)
            {
                _Shot0_C.CreateStart();
            }
            else
            {
                _Shot0_C.CreateEnd();
            }
        }
    }

    public void Shot0Get(Vector3 Pos, Quaternion Rotation)
    {
        GameObject Obj = ShotObjPool.Get();
        Transform ObjTransform = Obj.transform;
        ObjTransform.position = Pos;
        ObjTransform.rotation = Rotation;
        Obj.GetComponent<ShotObjManager>().Scripts[0].enabled = true;
    }
    
    void ResetScript(GameObject Obj)
    {
        ShotObjManager ObjManager = Obj.GetComponent<ShotObjManager>();
        foreach (MonoBehaviour Script in ObjManager.Scripts)
        {
            Script.enabled = false;
        }
    }
}
