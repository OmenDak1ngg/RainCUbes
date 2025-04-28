using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;
    [SerializeField] private float _spawnRate = 0.5f;

    private ObjectPool<Cube> _pool;
    private Bounds _spawnAreaBounds;

    private void Awake()
    {
        _spawnAreaBounds = _spawnArea.GetComponent<Collider>().bounds;

        _pool = new ObjectPool<Cube>(
           createFunc: () => Instantiate(_prefab),
           actionOnGet: (cube) => ActionOnGet(cube),
           actionOnRelease: (cube) => cube.gameObject.SetActive(false),
           actionOnDestroy: (cube) => Destroy(cube),
           collectionCheck: true,
           defaultCapacity: _poolCapacity,
           maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _spawnRate);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = new Vector3(
            Random.Range(_spawnAreaBounds.min.x, _spawnAreaBounds.max.x), _spawnArea.transform.position.y  ,0);

        cube.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.UnmarkReleased();
        cube.gameObject.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    public IEnumerator ReleaseWithDelay(Cube cube, float delay, ColorChanger _colorChanger)
    {
        yield return new WaitForSeconds(delay);
        _colorChanger.ResetMaterial(cube);
        _pool.Release(cube);
    }
}
