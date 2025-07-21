using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Vector3 _direction;
    private Vector3 _startPosition;
    private float _speed;

    public void Init(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        _startPosition = transform.position;
        StartCoroutine(Move(_direction));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Snake>(out Snake snake))
        {
            snake.Die();
        }
        else if (other.TryGetComponent<Bone>(out Bone bone))
        {
            bone.Demolish();
        }

        gameObject.SetActive(false);
    }

    private IEnumerator Move(Vector3 direction)
    {
        while (gameObject.activeSelf == true)
        {
            _startPosition += direction * (_speed * Time.deltaTime);
            transform.position = _startPosition;

            yield return null;
        }
    }
}
