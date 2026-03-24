using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot1_M : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Transform ModelTransform;

    //public ShotPool _ShotPool;

    public Vector3 FirstPos;
    public Vector3 TargetPos;
    public Transform PlayerTransform;

    [SerializeField] private float posValue = 0;
    const float POS_TIME = 1.5f;

    private float speedValue = 0;
    const float SPEED_TIME = 2.5f;
    const int SPEED_MAX = 50;

    private bool canGoPlayer = false;

    const int TIME_WAIT = 2;

    const float DELTA = 5.5f;

    void OnEnable()
    {
        posValue = 0;
        speedValue = 0;
        canGoPlayer = false;
    }

    void Update()
    {
        if (posValue < 1)
        {
            posValue += Time.deltaTime / POS_TIME;
            posValue = Mathf.Clamp01(posValue);
            _Transform.position = Vector3.Lerp(FirstPos, TargetPos, Mathf.Sqrt(posValue));
            //_Transform.position = Vector3.Lerp(FirstPos, TargetPos, posValue);
            if (posValue >= 1)
            {
                //_Transform.LookAt(PlayerTransform.position);
                StartCoroutine(GoStart());
            }
        }
        else
        {
            if (!canGoPlayer) return;

            speedValue += Time.deltaTime / SPEED_TIME;
            _Transform.position += Mathf.Lerp(0, SPEED_MAX, speedValue) * _Transform.forward * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        ModelTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    IEnumerator GoStart()
    {
        yield return new WaitForSeconds(TIME_WAIT);

        _Transform.LookAt(PlayerTransform.position + new Vector3(Random.Range(-DELTA, DELTA), 0, Random.Range(-DELTA, DELTA)));
        canGoPlayer = true;
    }
}
