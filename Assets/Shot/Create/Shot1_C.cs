using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Shot1_C : MonoBehaviour
{
    [SerializeField] ShotPool _ShotPool;

    [SerializeField] Transform _Transform;
    [SerializeField] Transform PlayerTransform;

    private bool isCoroutine;

    const float TIME_SHORT = 0.05f;
    const float TIME_LONG = 1;

    private int[] distance = { 36, 30, 24, 18, 12 };
    const int HALF_CIRCLE_NUM = 20;

    const int DEG_TO_RAD = 180;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CreateStart()
    {
        float deltaX = PlayerTransform.position.x - _Transform.position.x;
        float deltaY = PlayerTransform.position.y - _Transform.position.y;

        _Transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg);
    }

    IEnumerator ShotCoroutine()
    {
        while (isCoroutine)
        {
            for (int i = 0; i < distance.Length; i++)
            {
                for (int j = 0; j < HALF_CIRCLE_NUM; j++)
                {
                    float angle = DEG_TO_RAD * (j / HALF_CIRCLE_NUM);

                    GameObject Obj = _ShotPool.ShotObjPool.Get();
                    Shot1_M _Shot1_M = Obj.GetComponent<Shot1_M>();
                    _Shot1_M.FirstPos = _Transform.position;
                    _Shot1_M.TargetPos = _Transform.right * distance[j] * Mathf.Cos(angle * Mathf.Deg2Rad) + _Transform.up * distance[j] * Mathf.Sin(angle * Mathf.Deg2Rad);
                    _Shot1_M.PlayerTransform = PlayerTransform;

                    yield return new WaitForSeconds(TIME_SHORT);
                }
            }

            yield return new WaitForSeconds(TIME_LONG);
        }
    }
}
