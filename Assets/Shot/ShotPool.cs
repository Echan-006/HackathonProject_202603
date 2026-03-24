using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotPool : MonoBehaviour
{
    public ObjectPool<GameObject> ShotObjPool;
    [SerializeField] GameObject ShotObj;
    //MonoBehaviour[] MoveScripts;

    [SerializeField] PlayerMove _PlayerMove;

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
                    ShotParent _ShotParent = Obj.GetComponent<ShotParent>();
                    _ShotParent._ShotPool = this;
                    _ShotParent._PlayerMove = _PlayerMove;
                    foreach (GameObject Obj_ShotObjManager in _ShotParent.Objects)
                    {
                        Obj_ShotObjManager.SetActive(false);
                    }
                    poolNum++;
                    return Obj;
                },
                actionOnGet: Obj =>
                {
                    ResetObj(Obj);
                    actionNum++;
                    Obj.SetActive(true);
                },
                actionOnRelease: Obj =>
                {
                    ResetObj(Obj);
                    actionNum--;
                    Obj.SetActive(false);
                },
                actionOnDestroy: Obj =>
                {
                    ResetObj(Obj);
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
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    if (!_Shot0_C.isCoroutine)
        //    {
        //        _Shot0_C.CreateStart();
        //    }
        //    else
        //    {
        //        _Shot0_C.CreateEnd();
        //    }
        //}
    }

    //public void Shot0Get(Vector3 Pos, Quaternion Rotation)
    //{
    //    GameObject Obj = ShotObjPool.Get();
    //    Transform ObjTransform = Obj.transform;
    //    ObjTransform.position = Pos;
    //    ObjTransform.rotation = Rotation;
    //    //Obj.GetComponent<ShotObjManager>().Scripts[0].enabled = true;
    //    Obj.GetComponent<ShotObjManager>().Objects[0].SetActive(true);
    //}
    
    void ResetObj(GameObject Obj)
    {
        ShotParent _ShotParent = Obj.GetComponent<ShotParent>();
        foreach (GameObject Obj_ShotParent in _ShotParent.Objects)
        {
            Obj_ShotParent.SetActive(false);
        }
        //foreach (MonoBehaviour Script in ObjManager.Scripts)
        //{
        //    Script.enabled = false;
        //}
    }
}
