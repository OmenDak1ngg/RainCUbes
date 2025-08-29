using TMPro;
using UnityEngine;

public class CounterDisplayer<T> : MonoBehaviour where T : ExplodableObject
{

    [SerializeField] public TextMeshProUGUI _spawnedObjectsText;
    [SerializeField] public TextMeshProUGUI _createdObjectsText;
    [SerializeField] public TextMeshProUGUI _activeObjectsText;

    [SerializeField] private ExplodableObjectsSpawner<T> _spawner;

    private void OnEnable()
    {
        _spawner.ObjectCreated += UpdateCreatedCounter;
        _spawner.ObjectGetted += UpdateSpawnedCounter;
        _spawner.ObjectGetted += UpdateActiveCounter;
        _spawner.ObjectReleased += UpdateActiveCounter;
    }

    private void OnDisable()
    {
        _spawner.ObjectCreated -= UpdateCreatedCounter;
        _spawner.ObjectGetted -= UpdateSpawnedCounter;
        _spawner.ObjectGetted -= UpdateActiveCounter;
        _spawner.ObjectReleased -= UpdateActiveCounter;
    }

    private void Awake()
    {
        UpdateActiveCounter();
        UpdateCreatedCounter();
        UpdateSpawnedCounter();
    }

    private void UpdateSpawnedCounter()
    {
        _spawnedObjectsText.text = $"{_spawner.CountOfSpawnedObjects}";
    }

    private void UpdateCreatedCounter()
    {
        _createdObjectsText.text = $"{_spawner.CountOfCreatedObjects}";
    }

    private void UpdateActiveCounter()
    {
        _activeObjectsText.text = $"{_spawner.CountOfAtiveObjects}";
    }
}
