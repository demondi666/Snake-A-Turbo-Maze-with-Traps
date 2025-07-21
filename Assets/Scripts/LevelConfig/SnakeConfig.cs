using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Configs/Snake/SnakeConfig", fileName = "SnakeConfig")]
public class SnakeConfig : ScriptableObject
{
    [SerializeField] private Snake _prefabHead;
    [SerializeField] private Bone _prefabBone;
    [SerializeField] private Vector3 _spawnPointSnake;

    [field: SerializeField, Range(1, 10)] private int _numberBones;

    public Snake PrefabHead => _prefabHead;
    public Bone PrefabBone => _prefabBone;
    public Vector3 SpawnPointSnake => _spawnPointSnake;
    public int NumberBones => _numberBones;
}
