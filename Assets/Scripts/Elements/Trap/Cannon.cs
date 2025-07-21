using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Trap
{
    [SerializeField] private Shell _shellPrefab;
    [SerializeField] private ParticleSystem _shot;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _delayBetweenShoot;
    [SerializeField] private float _speedShell;
    [SerializeField] private AudioClip _shotAudio;

    private Coroutine _shooting;
    private bool _isShoot;
    private CustomPool<Shell> _shellPool;
    private List<Shell> _shells = new List<Shell>();

    private void Start()
    {
        _shellPool = new CustomPool<Shell>(_shellPrefab, "Shells");
    }

    private void OnEnable()
    {
        _isShoot = true;
        _shooting = StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        if (_shooting != null)
        {
            StopCoroutine(_shooting);
            _isShoot = false;
        }

        foreach (var shell in _shells)
        {
            if(shell!=null)
                shell.gameObject.SetActive(false);
        }

    }

    private IEnumerator Shoot()
    {
        while (_isShoot)
        {
            yield return new WaitForSeconds(_delayBetweenShoot);
            Animate();
        }
    }

    protected override void Animate()
    {
        Shell shell = _shellPool.Get();

        if (!_shells.Contains(shell))
        {
            _shells.Add(shell);
        }

        shell.transform.position = _shootPoint.position;
        Vector3 direction = -_shootPoint.up;
        shell.Init(direction, _speedShell);
        _shot.Play();
        AudioSource.PlayOneShot(_shotAudio);
    }
}
