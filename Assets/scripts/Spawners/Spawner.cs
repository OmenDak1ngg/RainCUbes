using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    protected ObjectPool<T> Pool;

    public event Action ObjectGetted;

    protected virtual void Start()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => OnInstantiateObject(),
            actionOnGet:(T poolObject) => OnObjectGet(poolObject),
            actionOnRelease:(poolObject) => OnObjectRelease(poolObject),
            actionOnDestroy:(poolObject) => OnDestroyObject(poolObject)
            );
    }

    protected virtual T OnInstantiateObject()
    {
        T newObject = Instantiate(_prefab);
        
        return newObject;
    }

    protected virtual void  OnObjectGet(T poolObject)
    {
        poolObject.gameObject.SetActive(true);
    }

    protected virtual void OnObjectRelease(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyObject(T poolObject)
    {
        Destroy(poolObject);
    }

    protected virtual void ReleaseObject(T poolObject)
    {
        Pool.Release(poolObject);
    }
}
