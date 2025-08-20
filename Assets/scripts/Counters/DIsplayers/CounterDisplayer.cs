using TMPro;
using UnityEngine;

public class CounterDisplayer<T> : MonoBehaviour where T : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI _spawnedObjectsText;
    [SerializeField] public TextMeshProUGUI _createdObjectsText;
    [SerializeField] public TextMeshProUGUI _activeObjectsText;

    [SerializeField] private Counter<T> _counter;

    private void OnEnable()
    {
        _counter.CountUpdated += UpdateCounter;
    }

    private void OnDisable()
    {
        _counter.CountUpdated -= UpdateCounter;
    }

    private void Awake()
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _spawnedObjectsText.text = $"{_counter.CountOfSpawnedObjects}";
        _createdObjectsText.text = $"{_counter.CountOfCreatedObjects}";
        _activeObjectsText.text = $"{_counter.CountOfActiveObjects}";
    }
}
