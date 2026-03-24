using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotEffectPool : MonoBehaviour
{
    public ObjectPool<GameObject> EffectPool;
    [SerializeField] GameObject Effect;

    void Start()
    {
        EffectPool = new ObjectPool<GameObject>
            (
                createFunc: () =>
                {
                    GameObject Obj = Instantiate(Effect);
                    return Obj;
                },
                actionOnGet: Obj => Obj.SetActive(true),
                actionOnRelease: Obj => Obj.SetActive(false),
                actionOnDestroy: Obj => Destroy(Obj),
                collectionCheck: true,
                defaultCapacity: 1,
                maxSize: 4
            );
    }

    void Update()
    {
        
    }

    public void ObjGet(Vector3 Pos)
    {
        GameObject Obj = EffectPool.Get();
        Transform ObjTransform = Obj.transform;
        ObjTransform.rotation = Quaternion.Euler(0, 0, 0);
        ObjTransform.position = Pos;
    }
}
