using TMPro;
using UnityEngine;

public class CounterDisplayer<T> : MonoBehaviour where T : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _counterText;

    [SerializeField] private Counter<T> _counter;

    private void OnEnable()
    {
        _counter.CountIncreased += UpdateCounter;
    }

    private void OnDisable()
    {
        _counter.CountIncreased -= UpdateCounter;
    }

    private void UpdateCounter()
    {
        _counterText.text = $"{_counter.CountOfObjects}";
    }
}
