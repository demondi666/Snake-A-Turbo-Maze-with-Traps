using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleModel : MonoBehaviour
{
    [SerializeField] private List<FragmentSplit> _frarments;
    [SerializeField] private AudioClip _deadSound;

    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_deadSound);

        foreach (var fragment in _frarments)
        {
            fragment.Activate();

            if (fragment.Rigidbody != null)
            {
                fragment.Rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}
