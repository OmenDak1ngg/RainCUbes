using UnityEngine;

public class RemovalArea : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _minTimeToDestroy;
    [SerializeField] private float _maxTimeToDestroy;

    private void OnTriggerEnter(Collider collider)
    {
        float delay = Random.Range(_minTimeToDestroy, _maxTimeToDestroy);
        _colorChanger.SetRandomMaterial(collider.gameObject);
        _cubeSpawner.ReleaseWithDelay(collider.gameObject, delay, _colorChanger);
    }
}
