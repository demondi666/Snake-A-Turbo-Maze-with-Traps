using System.Collections;
using UnityEngine;
using Zenject;

public class Bone : MonoBehaviour
{
    private const float TimeToDestruction = 4f;

    [SerializeField] private DestructibleModel _model;
    [SerializeField] private float _durationMagnification;

    private Snake _head;
    private DestructibleModel _createdModel;
    private Rigidbody _rigidbody;
    private BoxCollider _collider;
    private Vector3 _startScale;
    private Vector3 _endScale;
    private Coroutine _increase;

    public Rigidbody Rigidbody => _rigidbody;
    public BoxCollider Collider => _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
         _increase = StartCoroutine(EnlargeBone());
    }

    private void OnDisable()
    {
        if (_increase != null)
            StopCoroutine(_increase);
    }

    [Inject]
    private void Construct(Snake head)
    {
        _head = head;
    }

    public void Destruction()
    {
        gameObject.SetActive(false);
        _createdModel = Instantiate(_model, transform.position, transform.rotation);
        Destroy(_createdModel.gameObject, TimeToDestruction);
    }

    public void Demolish()
    {
        _head.OnBoneDemolish(this);
    }

    private IEnumerator EnlargeBone()
    {
        float timeElapsed = 0f;

        _endScale = transform.localScale;

        while (timeElapsed < _durationMagnification)
        {
            float time = timeElapsed / _durationMagnification;

            transform.localScale = Vector3.Lerp(_startScale, _endScale, time);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localScale = _endScale;
    }
}
