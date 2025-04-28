using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RemovalArea : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _minTimeToDestroy;
    [SerializeField] private float _maxTimeToDestroy;

    private void OnTriggerEnter(Collider collider)
    {

        Cube cube = collider.GetComponent<Cube>();
        
        if(cube.IsReleased)
            return;

        float delay = Random.Range(_minTimeToDestroy, _maxTimeToDestroy);
        
        cube.MarkReleased();

        _colorChanger.SetRandomMaterial(cube);
        StartCoroutine(_cubeSpawner.ReleaseWithDelay(cube, delay, _colorChanger));
    }
}
