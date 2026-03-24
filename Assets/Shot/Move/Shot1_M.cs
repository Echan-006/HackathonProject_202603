using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot1_M : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Transform ModelTransform;

    public ShotPool _ShotPool;

    public Vector3 FirstPos;
    public Vector3 TargetPos;
    public Transform PlayerTransform;

    Vector3 PlayerTarget;

    private float posValue = 0;
    const float POS_TIME = 1.5f;

    private float speedValue = 0;
    const float SPEED_TIME = 2.5f;
    const int SPEED_MAX = 50;

    void OnEnable()
    {
        posValue = 0;
        speedValue = 0;
    }

    void Update()
    {
        if (posValue < 1)
        {
            posValue += Time.deltaTime / POS_TIME;
            posValue = Mathf.Clamp01(posValue);
            _Transform.position = Vector3.Lerp(FirstPos, TargetPos, Mathf.Sqrt(posValue));
            if (posValue >= 1)
            {
                _Transform.LookAt(PlayerTransform.position);
            }
        }
        else
        {
            speedValue += Time.deltaTime / SPEED_TIME;
            _Transform.position += Mathf.Lerp(0, SPEED_MAX, speedValue) * _Transform.forward * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        ModelTransform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
