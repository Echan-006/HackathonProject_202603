using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AttackPool : MonoBehaviour
{
    public ObjectPool<GameObject> AttackObjPool;
    [SerializeField] GameObject AttackObj;
    [SerializeField] Transform Enemy;

    private int num;
    const int NUM_MAX = 15;

    const int TIME_MIN = 5;
    const int TIME_MAX = 10;

    const float DISTANCE_MIN = 10;
    const float DISTANCE_MAX = 25;
    const float DISTANCE_POW = 1.5f;

    const float Y_MIN = 0.5f;
    const float Y_MAX = 13.5f;
    const float Y_POW = 1.5f;

    void Start()
    {
        AttackObjPool = new ObjectPool<GameObject>
            (
                createFunc: () =>
                {
                    GameObject Obj = Instantiate(AttackObj);
                    AttackScript _AttackScript = Obj.GetComponent<AttackScript>();
                    _AttackScript.Enemy = Enemy;
                    _AttackScript._AttackPool = this;
                    return Obj;
                },
                actionOnGet: Obj =>
                {
                    Obj.SetActive(true);
                    num++;
                },
                actionOnRelease: Obj =>
                {
                    Obj.SetActive(false);
                    num--;
                },
                actionOnDestroy: Obj =>
                {
                    Destroy(Obj);
                    num--;
                },
                collectionCheck: true,
                defaultCapacity: NUM_MAX,
                maxSize: 20
            );

        StartCoroutine(CreateCoroutine());
    }

    void Update()
    {
        
    }

    IEnumerator CreateCoroutine()
    {
        while (true)
        {
            if(num < NUM_MAX)
            {
                float angle = Random.Range(0f, 360f);
                float randomValue_1 = Random.value;
                float distance = DISTANCE_MIN + Mathf.Lerp(0, DISTANCE_MAX, Mathf.Pow(randomValue_1, DISTANCE_POW));
                float randomValue_2 = Random.value;
                Vector3 Pos = new Vector3(distance * Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Lerp(Y_MAX, Y_MIN, Mathf.Pow(randomValue_2, Y_POW)), distance * Mathf.Sin(angle * Mathf.Deg2Rad));

                GameObject Obj = AttackObjPool.Get();
                Obj.transform.position = Pos;
            }

            yield return new WaitForSeconds(Random.Range(TIME_MIN, TIME_MAX));
        }
    }
}
