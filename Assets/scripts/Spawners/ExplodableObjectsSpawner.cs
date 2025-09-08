using System;
using UnityEngine;
using UnityEngine.Pool;

public class ExplodableObjectsSpawner<T> : MonoBehaviour where T : ExplodableObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private ColorChanger _colorChanger;

    protected ObjectPool<T> Pool;

    public int CountOfSpawnedObjects { get;private set; }
    public int CountOfCreatedObjects { get;private set; }
    public int CountOfAtiveObjects { get;private set; }

    public event Action ObjectGetted;
    public event Action ObjectCreated;
    public event Action ObjectReleased;

    protected virtual void Start()
    {
        CountOfAtiveObjects = 0;
        CountOfCreatedObjects = 0;
        CountOfSpawnedObjects = 0;

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

        newObject.SetColorChanger( _colorChanger);

        CountOfCreatedObjects += 1;
        ObjectCreated?.Invoke();

        return newObject;
    }

    protected virtual void  OnObjectGet(T poolObject)
    {
        CountOfAtiveObjects += 1;
        CountOfSpawnedObjects += 1;

        ObjectGetted?.Invoke();
        poolObject.gameObject.SetActive(true);
    }

    protected virtual void OnObjectRelease(T poolObject)
    {
        CountOfAtiveObjects -= 1;

        poolObject.gameObject.SetActive(false);

        ObjectReleased?.Invoke();
    }

    protected virtual void OnDestroyObject(T poolObject)
    {
        Destroy(poolObject);
    }

    protected virtual void ReleaseObject(T poolObject)
    {
        Pool.Release(poolObject);
        
        if (poolObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody) == false)
            return;

        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.linearVelocity = Vector3.zero;
    }
}
