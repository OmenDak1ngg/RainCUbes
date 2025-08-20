using System;
using TMPro;
using UnityEngine;

public class Counter<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;

    public int CountOfSpawnedObjects { get; private set; }
    public int CountOfCreatedObjects { get; private set; }
    public int CountOfActiveObjects { get; private set; }

    

    public event Action CountUpdated;

    private void OnEnable()
    {
        _spawner.ObjectGetted += IncreaseCountOfSpawnedObjects;
        _spawner.ObjectCreated += IncreaseCountOfCreatedObjects;
        _spawner.ObjectReleased += DecreaseCountOfActiveObjects;
    }

    private void OnDisable()
    {
        _spawner.ObjectGetted -= IncreaseCountOfSpawnedObjects;
        _spawner.ObjectCreated -= IncreaseCountOfCreatedObjects;
        _spawner.ObjectReleased -= DecreaseCountOfActiveObjects;
    }

    private void Awake()
    {
        CountOfSpawnedObjects = 0;
        CountOfCreatedObjects = 0;
        CountOfActiveObjects = 0;
    }

    private void IncreaseCountOfSpawnedObjects()
    {
        CountOfSpawnedObjects += 1;
        CountOfActiveObjects += 1;
        CountUpdated?.Invoke();
    }

    private void IncreaseCountOfCreatedObjects()
    {
        CountOfCreatedObjects += 1;
        CountUpdated?.Invoke();
    }

    private void DecreaseCountOfActiveObjects()
    {
        CountOfActiveObjects -= 1;
        CountUpdated?.Invoke();
    }
}
