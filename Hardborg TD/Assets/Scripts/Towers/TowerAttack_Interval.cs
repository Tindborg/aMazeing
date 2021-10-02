using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TowerAttack_Interval : MonoBehaviour
{

    [SerializeField]
    private TowerAimer _Aimer = default;

    [SerializeField]
    private float _Cooldown = 1f;

    public UnityEvent UE_OnFire;

    private Coroutine _attackTask = null;

    private bool _loaded = true;

    private void Start()
    {
        _Aimer.UE_OnAcquireTarget.AddListener(StartAttacking);
    }

    private void StartAttacking()
    {
        StartCoroutine(Coroutine_Attack());
    }

    private void StopAttacking()
    {
        StopAllCoroutines();
    }

    private IEnumerator Coroutine_Attack()
    {
        while (true)
        {
            if (_loaded) Fire();
        }
    }

    private void Fire()
    {
        print("onfire");
        UE_OnFire.Invoke();
        _loaded = false;
        Invoke("Reload", _Cooldown);
    }

    private void Reload()
    {
        _loaded = true;
    }

}
