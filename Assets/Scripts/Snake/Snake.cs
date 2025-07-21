using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    [SerializeField] private CharacterConfig _config;
    [SerializeField] private DestructibleModel _destructibleModel;
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private AudioClip _soundOfCrawling;
    [SerializeField] private AudioClip _soundOfEating;

    private Input _inputSnake;
    private CharacterController _controller;
    private List<Bone> _tails;
    private SnakeStateMachine _stateMachine;
    private CinemachineVirtualCamera _camera;
    private AudioSource _audioSource;
    private int _coins;

    public UnityAction Eating;
    public UnityAction OnDied;
    public UnityAction WalkedThroughDoor;
    public UnityAction<bool> OnSpeedChange;
    public UnityAction OnPickUpCoin;

    public Input InputSnake => _inputSnake;
    public CharacterController Controller => _controller;
    public List<Bone> Tails => _tails;
    public CharacterConfig Config => _config;
    public DestructibleModel DestructibleModel => _destructibleModel;
    public CharacterView View => _characterView;
    public AudioSource AudioSource => _audioSource;
    public AudioClip SoundOfEating => _soundOfEating;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _controller = GetComponent<CharacterController>();
        _inputSnake = new Input();
        _camera = FindObjectOfType<CinemachineVirtualCamera>();
        _characterView.Initialize();
        _stateMachine = new SnakeStateMachine(this);
    }

    private void FixedUpdate()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void OnEnable()
    {
        Follow();
        _inputSnake.Enable();
        OnPickUpCoin+=CoinsChange;
    }

    private void OnDisable()
    {
        _inputSnake.Disable();
        OnPickUpCoin -= CoinsChange;
    }

    public void OnBoneDemolish(Bone bone)
    {
        int index = _tails.IndexOf(bone);

        if (index ==-1)
        {
            Debug.LogWarning("Object not found in the list.");
            return;
        }

        for (int i = index; i < _tails.Count; i++)
        {
            if (i < 0)
                return;

            Bone createdBone = _tails[i];

            if (createdBone != null)
            {
                createdBone.Destruction();
            }

            _tails.RemoveAt(i);
            i--;
        }

        if (_tails.Count == 0)
        {
            Die();
        }
    }

    public void Initialize(List<Bone> tails)
    {
        _tails = tails;
    }

    public void Die()
    {
        OnDied?.Invoke();
    }

    public int GetCoins()
    {
        return _coins;
    }

    private void Follow()
    {
        _camera.Follow = transform;
        _camera.LookAt = transform;
    }

    private void CoinsChange()
    {
        _coins++;
    }
}
