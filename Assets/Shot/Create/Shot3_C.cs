using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shot3_C : MonoBehaviour
{
    [SerializeField] ShotPool _ShotPool;

    [SerializeField] Transform _Transform;

    private bool isCoroutine;

    const int SHOT_NUM = 8;
    const float TIME = 0.2f;

    private float angleX;
    const int ANGLE_SPEED = 25;

    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    if (!isCoroutine)
        //    {
        //        CreateStart();
        //    }
        //    else
        //    {
        //        CreateEnd();
        //    }
        //}

        angleX += ANGLE_SPEED * Time.deltaTime;
    }

    public void CreateStart()
    {
        angleX = Random.Range(0, 360);
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
            for (int i = 0; i < SHOT_NUM; i++)
            {
                float angle_1 = Mathf.Acos(1 - 2f * (i + 0.5f) / SHOT_NUM);

                float angleBase = Mathf.PI * (3f - Mathf.Sqrt(5f));
                float angle_2 = i * angleBase;

                Vector3 Direction = new Vector3(Mathf.Sin(angle_1) * Mathf.Cos(angle_2), Mathf.Cos(angle_1), Mathf.Sin(angle_1) * Mathf.Sin(angle_2)).normalized;

                GameObject Obj = _ShotPool.ShotObjPool.Get();
                GameObject Shot = Obj.GetComponent<ShotParent>().Objects[3];
                Shot.SetActive(true);
                Transform ShotTransform = Shot.transform;
                ShotTransform.position = _Transform.position;
                ShotTransform.rotation = Quaternion.LookRotation(Quaternion.Euler(angleX, 0, 0) * Direction);
            }
            yield return new WaitForSeconds(TIME);
        }
    }
}
