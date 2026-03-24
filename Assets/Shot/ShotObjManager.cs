using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotObjManager : MonoBehaviour
{
    //public ShotPool _ShotPool;
    ////public MonoBehaviour[] Scripts;
    //public GameObject[] Objects;

    //public PlayerMove _PlayerMove;
    [SerializeField] ShotParent _ShotParent;
    [SerializeField] Transform _Transform;

    const int Y_MIN = -1;
    const int Y_MAX = 40;
    const int X_Z_MAX = 51;

    [SerializeField] private bool isRelease = false;
    private float releaseValue = 0;
    const float RELEASE_TIME = 0.75f;
    private float scaleValue = 0;
    const int SCALE_MAX = 1;

    void OnEnable()
    {
        isRelease = false;
        releaseValue = 0;
        scaleValue = SCALE_MAX;
        _Transform.localScale = new Vector3(SCALE_MAX, SCALE_MAX, SCALE_MAX);
    }

    void Update()
    {
        if (_Transform.position.y < Y_MIN || _Transform.position.y > Y_MAX || Mathf.Abs(_Transform.position.x) > X_Z_MAX || Mathf.Abs(_Transform.position.z) > X_Z_MAX)
        {
            isRelease = true;
        }

        if (isRelease)
        {
            releaseValue += Time.deltaTime / RELEASE_TIME;
            releaseValue = Mathf.Clamp01(releaseValue);
            scaleValue = Mathf.Lerp(SCALE_MAX, 0, releaseValue);
        }

        if (releaseValue >= 1)
        {
            _ShotParent._ShotPool.ShotObjPool.Release(_ShotParent.gameObject);
        }

        _Transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerToDamage")) return;

        _ShotParent._PlayerMove.Damage();
        _ShotParent._ShotPool.ShotObjPool.Release(_ShotParent.gameObject);
    }
}
