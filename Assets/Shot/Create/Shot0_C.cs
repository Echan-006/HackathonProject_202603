using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot0_C : MonoBehaviour
{
    [SerializeField] ShotPool _ShotPool;

    [SerializeField] Transform _Transform;

    public bool isCoroutine = false;
    const float TIME = 0.1f;
    const int CREATE_NUM = 5;

    const float ROTATION_Y_MIN = -40f;
    const float ROTATION_Y_MAX = 60f;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void CreateStart()
    {
        isCoroutine = true;
        StartCoroutine(ShotCoroutine());
    }

    public void CreateEnd()
    {
        isCoroutine = false;
        StopCoroutine(ShotCoroutine());
    }

    IEnumerator ShotCoroutine()
    {
        while (isCoroutine)
        {
            yield return new WaitForSeconds(TIME);

            for (int i = 0; i < CREATE_NUM; i++)
            {
                _ShotPool.Shot0Get(_Transform.position, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(ROTATION_Y_MIN, ROTATION_Y_MAX), 0));
            }
        }
    }
}
