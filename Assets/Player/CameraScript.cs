using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] Transform TargetTransform;
    [SerializeField] PlayerMove _PlayerMove;

    private float noiseValue;
    const int NOISE_SPEED = 10;
    const int DISTANCE_MAX = 3;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        _Transform.LookAt(TargetTransform);

        noiseValue += NOISE_SPEED * Time.deltaTime;
        Vector3 DeltaPos = Mathf.Lerp(0, DISTANCE_MAX, _PlayerMove.damagePerformanceTime)
                                      * (_Transform.up * Mathf.PerlinNoise(noiseValue, noiseValue) + _Transform.right * Mathf.PerlinNoise1D(noiseValue));
        _Transform.localPosition = DeltaPos;
    }
}
