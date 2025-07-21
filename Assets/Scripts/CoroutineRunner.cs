using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private List<Coroutine> _coroutines; 

    public static CoroutineRunner Instance { get; private set;}

    private void Awake()
    {
        _coroutines = new List<Coroutine>();

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Coroutine ActivateCoroutine(IEnumerator routine)
    {
        Coroutine coroutine = StartCoroutine(routine);
        _coroutines.Add(coroutine);

        return coroutine;
    }

    public void DeactivatedCoroutines()
    {
        foreach (var coroutine in _coroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        _coroutines.Clear();
    }
}
