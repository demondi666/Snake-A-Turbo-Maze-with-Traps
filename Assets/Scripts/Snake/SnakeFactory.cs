using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SnakeFactory
{
    private Snake _createdHead;
    private List<Bone> _createdBones;

    private DiContainer _container;
    private Bone _prefab;

    public SnakeFactory(DiContainer container)
    {
        _container = container;
        _createdBones = new List<Bone>();
    }

    public Snake GetSnake(SnakeConfig config)
    {
        _prefab = config.PrefabBone;

        _createdHead = CreateHead(config.PrefabHead, config.SpawnPointSnake);
        _createdBones = CreateBones(config.NumberBones, config.PrefabBone, config.SpawnPointSnake);

        _createdHead.Initialize(_createdBones);
        return _createdHead;
    }

    public Bone GetBone()
    {
        Bone bone = _container.InstantiatePrefabForComponent<Bone>(_prefab);
        _createdBones.Add(bone);

        return bone;
    }

    private Snake CreateHead(Snake prefab, Vector3 spawnPoint)
    {
        Snake head = _container.InstantiatePrefabForComponent<Snake>(prefab, spawnPoint, Quaternion.identity, null);
        _container.Bind<Snake>().FromInstance(head).AsSingle().NonLazy();
        
        return head;
    }

    private List<Bone> CreateBones(int numberBones, Bone prefab, Vector3 spawnPosition)
    {
        for (int i = 0; i < numberBones; i++)
        {
            Bone bone = _container.InstantiatePrefabForComponent<Bone>(prefab, spawnPosition, Quaternion.identity, null);
            _createdBones.Add(bone);
        }
        
        return _createdBones;
    }
}
