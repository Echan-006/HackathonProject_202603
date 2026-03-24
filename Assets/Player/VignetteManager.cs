using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteManager : MonoBehaviour
{
    [SerializeField] PlayerMove _PlayerMove;
    [SerializeField] Volume _Volume;
    Vignette _Vignette;

    private float intensityNormal;
    const float INTENSITY_MAX = 0.5f;
    const float INTENSITY_MIN_PROPORTION = 0.7f;
    private float intensityNormalValue = 0;
    const float INTENSITY_SPEED = 0.25f;
    const float INTENSITY_DAMAGE = 0.55f;

    void Start()
    {
        _Volume.profile.TryGet(out _Vignette);
    }

    void Update()
    {
        float lifeValue = (float)_PlayerMove.life / _PlayerMove.lifeMax;

        intensityNormalValue += INTENSITY_SPEED * Time.deltaTime / lifeValue;
        if (intensityNormalValue >= 1)
        {
            intensityNormalValue -= 1;
        }
        float intensityBase = INTENSITY_MAX * (1 - Mathf.Sqrt(lifeValue));
        intensityNormal = Mathf.Lerp(intensityBase, intensityBase * INTENSITY_MIN_PROPORTION, Mathf.Sqrt(intensityNormalValue));

        _Vignette.intensity.value = Mathf.Lerp(intensityNormal, INTENSITY_DAMAGE, _PlayerMove.damagePerformanceTime);
    }
}
