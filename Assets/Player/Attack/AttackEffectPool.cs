using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AttackEffectPool : MonoBehaviour
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
                    Obj.GetComponent<AttackEffect>()._AttackEffectPool = this;
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
}
