using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotPool : MonoBehaviour
{
    public ObjectPool<GameObject> ShotObjPool;
    [SerializeField] GameObject ShotObj;
    MonoBehaviour[] Scripts;

    [SerializeField] private int actionNum;  //使用中のオブジェクト数
    [SerializeField] private int poolNum;  //プールが持ってる全オブジェクト数

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
