using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] ParticleSystem _ParticleSystem;

    private int life;

    [SerializeField] GameObject DamageObj;
    public ObjectPool<GameObject> DamagePool;
    const int DAMAGE_NUM = 3;

    [SerializeField] Transform ModelTransform;
    private float noiseValue = 0;
    const int NOISE_SPEED = 1500;
    const int NOISE_X_BASE = 0;
    const int NOISE_Y_BASE = 100;
    const int NOISE_Z_BASE = 200;
    const int DISTANCE_MAX = 10;
    private float distanceValue = 0;
    const float DISTANCE_TIME = 0.5f;

    void Start()
    {
        DamagePool = new ObjectPool<GameObject>
            (
                createFunc: () =>
                {
                    GameObject Obj = Instantiate(DamageObj);
                    Obj.GetComponent<EnemyDamage>()._Enemy = this;
                    return Obj;
                },
                actionOnGet: Obj =>
                {
                    Obj.SetActive(true);
                    Obj.transform.position = _Transform.position;
                },
                actionOnRelease: Obj => Obj.SetActive(false),
                actionOnDestroy: Obj => Destroy(Obj),
                collectionCheck: true,
                defaultCapacity: 3,
                maxSize: 12
            );
    }

    void Update()
    {
        noiseValue += NOISE_SPEED * Time.deltaTime;
        if (distanceValue > 0)
        {
            distanceValue -= Time.deltaTime / DISTANCE_TIME;
        }
        distanceValue = Mathf.Clamp01(distanceValue);

        if (distanceValue <= 0)
        {
            if (_ParticleSystem.isPaused)
            {
                _ParticleSystem.Play();
            }
        }

        ModelTransform.localPosition = Mathf.Lerp(0, DISTANCE_MAX, distanceValue) * new Vector3(Mathf.PerlinNoise1D(NOISE_X_BASE + noiseValue) - 0.5f,
                                                                                                Mathf.PerlinNoise1D(NOISE_Y_BASE + noiseValue) - 0.5f,
                                                                                                Mathf.PerlinNoise1D(NOISE_Z_BASE + noiseValue) - 0.5f);
    }

    public void Damage()
    {
        life--;
        for (int i = 0; i < DAMAGE_NUM; i++)
        {
            DamagePool.Get();
        }
        _ParticleSystem.Pause();
        distanceValue = 1;
    }
}
