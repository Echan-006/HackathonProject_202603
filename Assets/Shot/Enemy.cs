using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform _Transform;
    [SerializeField] ParticleSystem _ParticleSystem;

    [SerializeField] Shot0_C _Shot0_C;
    [SerializeField] Shot1_C _Shot1_C;
    [SerializeField] Shot2_C _Shot2_C;
    [SerializeField] Shot3_C _Shot3_C;

    private bool[] once = { true, true, true, true };

    const int LIFE_1 = 12;
    const int LIFE_2 = 8;
    const int LIFE_3 = 4;

    private bool onceEnd = true;

    public int life { get; private set; } = 16;
    const int LIFE_MAX = 16;

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
        if (once[0])
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetKeyDown(KeyCode.Space))
            {
                _Shot0_C.CreateStart();
                once[0] = false;
            }
        }
        if (once[1])
        {
            if (life <= LIFE_1)
            {
                _Shot1_C.CreateStart();
                once[1] = false;
            }
        }
        if (once[2])
        {
            if (life <= LIFE_2)
            {
                _Shot2_C.CreateStart();
                once[2] = false;
            }
        }
        if (once[3])
        {
            if (life <= LIFE_3)
            {
                _Shot3_C.CreateStart();
                once[3] = false;
            }
        }

        if (life <= 0)
        {
            if (onceEnd)
            {
                _Shot0_C.CreateEnd();
                _Shot1_C.CreateEnd();
                _Shot2_C.CreateEnd();
                _Shot3_C.CreateEnd();
                onceEnd = false;
            }
        }

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
        if (life > 0)
        {
            life--;
        }

        for (int i = 0; i < DAMAGE_NUM; i++)
        {
            DamagePool.Get();
        }
        _ParticleSystem.Pause();
        distanceValue = 1;
    }
}
