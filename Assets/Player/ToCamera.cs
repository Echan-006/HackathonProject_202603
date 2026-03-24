using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCamera : MonoBehaviour
{
    [SerializeField] Transform _Transform;

    private float posX = 0;
    const int POS_X_SPEED = 25;
    const float POS_X_MAX = 3.5f;

    const int POS_Y = 1;
    const int POS_Z = 3;

    [SerializeField] PlayerMove _PlayerMove;

    private float noiseValue;
    const int NOISE_SPEED = 1500;
    const int DISTANCE_MAX = 20;

    void Start()
    {
        
    }

    void Update()
    {
        noiseValue += NOISE_SPEED * Time.deltaTime;
        Vector3 DeltaPos = Mathf.Lerp(0, DISTANCE_MAX, _PlayerMove.damagePerformanceTime)
                                      * (_Transform.up * (Mathf.PerlinNoise(noiseValue, noiseValue) - 0.5f) + _Transform.right * (Mathf.PerlinNoise1D(noiseValue) -0.5f));

        posX += POS_X_SPEED * -Input.GetAxis("Mouse X") * Time.deltaTime;
        posX = Mathf.Clamp(posX, -POS_X_MAX, POS_X_MAX);
        _Transform.localPosition = new Vector3(posX, POS_Y, POS_Z) + DeltaPos;
    }

    private void FixedUpdate()
    {
        //_Transform.localPosition = new Vector3(posX, POS_Y, POS_Z);
    }
}
