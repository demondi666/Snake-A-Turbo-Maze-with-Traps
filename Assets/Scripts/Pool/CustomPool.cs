using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _objects;
    private GameObject _container;

    public CustomPool(T prefab, string containerName)
    {
        _prefab = prefab;
        _objects = new List<T>();
        _container = new GameObject(containerName);
        T obj = GameObject.Instantiate(_prefab, _container.transform);
        obj.gameObject.SetActive(false);
        _objects.Add(obj);
    }
    public T Get()
    {
        T obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled && x is T);

        if (obj == null)
        {
            obj = Create();
        }
        obj.transform.localScale = _prefab.transform.localScale;
        obj.gameObject.SetActive(true);
        return obj;
    }
    private T Create()
    {
        T obj = GameObject.Instantiate(_prefab, _container.transform);
        _objects.Add(obj);
        return obj;
    }
}
