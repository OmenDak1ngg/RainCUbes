using System;
using UnityEngine;

public class Counter<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;

    public int CountOfObjects { get; private set; }

    public event Action CountIncreased;

    private void OnEnable()
    {
        _spawner.ObjectGetted += IncreaseCountOfObjects;
    }

    private void OnDisable()
    {
        _spawner.ObjectGetted -= IncreaseCountOfObjects;
    }

    private void Start()
    {
        CountOfObjects = 0;
    }

    private void IncreaseCountOfObjects()
    {
        CountOfObjects += 1;
        CountIncreased?.Invoke();
    }
}
