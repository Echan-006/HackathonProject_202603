using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot2_C : MonoBehaviour
{
    [SerializeField] ShotPool _ShotPool;
    [SerializeField] Transform _Transform;

    Shot2_M[] _Shot2_C = new Shot2_M[8];

    Quaternion RotationBase;

    private bool isCoroutine;

    private bool canMove;

    const float TIME = 0.55f;
    const int SHOT_NUM = 8;

    const int ROTATION = 45;
    [SerializeField] private float toRotation;
    const float SPEED = 0.75f;
    private int sign = 1;

    private float noiseValue;
    const int NOISE_SPEED = 250;

    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha2))
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

        //toRotation += sign * SPEED * Time.deltaTime;
        //if (Mathf.Abs(toRotation) > 1)
        //{
        //    sign *= -1;
        //}
        //toRotation = Mathf.Clamp(toRotation, -1, 1);
        noiseValue += NOISE_SPEED * Time.deltaTime;
    }

    public void CreateStart()
    {
        toRotation = Random.Range(0, 360);
        isCoroutine = true;
        canMove = true;
        StartCoroutine(ShotCoroutine());
    }

    public void CreateEnd()
    {
        isCoroutine = false;
    }

    IEnumerator ShotCoroutine()
    {
        //canMove = false;
        //StartCoroutine(MoveCoroutine());

        while (isCoroutine)
        {
            //RotationBase = Quaternion.Euler(ROTATION * toRotation, 0, 0);
            RotationBase = Quaternion.Euler(ROTATION * (2 * Mathf.PerlinNoise1D(noiseValue) - 1), 0, 0);
            int rotationRandom = Random.Range(0, 360);
            for (int i = 0; i < SHOT_NUM; i++)
            {
                GameObject Obj = _ShotPool.ShotObjPool.Get();
                ShotParent _ShotParent = Obj.GetComponent<ShotParent>();
                GameObject Shot = _ShotParent.Objects[2];
                Shot.SetActive(true);
                Transform ShotTransform = Shot.transform;
                ShotTransform.position = _Transform.position;
                ShotTransform.rotation = Quaternion.Euler(0, rotationRandom + 360 * ((float)i / SHOT_NUM), 0) * Quaternion.Inverse(RotationBase);

                _Shot2_C[i] = Shot.GetComponent<Shot2_M>();

                //yield return null;
            }
            //canMove = true;

            foreach (Shot2_M Script in _Shot2_C)
            {
                if (Script != null)
                {
                    Script._Shot_M_Straight.enabled = true;
                }
            }
            yield return new WaitForSeconds(TIME);
        }
        //RotationBase = Quaternion.Euler(ROTATION * toRotation, 0, 0);
        //int rotationRandom = Random.Range(0, 360);
        //for (int i = 0; i < SHOT_NUM; i++)
        //{
        //    GameObject Obj = _ShotPool.ShotObjPool.Get();
        //    ShotParent _ShotParent = Obj.GetComponent<ShotParent>();
        //    GameObject Shot = _ShotParent.Objects[2];
        //    Shot.SetActive(true);
        //    Transform ShotTransform = Shot.transform;
        //    ShotTransform.position = _Transform.position;
        //    ShotTransform.rotation = Quaternion.Euler(0, rotationRandom + 360 * ((float)i / SHOT_NUM), 0) * Quaternion.Inverse(RotationBase);

        //    _Shot2_C[i] = Shot.GetComponent<Shot2_M>();

        //    //yield return null;
        //}
        ////canMove = true;

        //foreach (Shot2_M Script in _Shot2_C)
        //{
        //    Script._Shot_M_Straight.enabled = true;
        //}
        //yield return new WaitForSeconds(TIME);

    }

    IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(TIME);
        yield return new WaitUntil(() => canMove);

        foreach (Shot2_M Script in _Shot2_C)
        {
            Script._Shot_M_Straight.enabled = true;
        }

        canMove = true;
        if (isCoroutine)
        {
            StartCoroutine(ShotCoroutine());
        }
    }
}
