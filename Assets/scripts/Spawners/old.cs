//using System.Collections;
//using UnityEngine;
//using UnityEngine.Pool;

//public class OLDCUBESPAWNER : MonoBehaviour
//{
//    [SerializeField] private Cube _prefab;
//    [SerializeField] private int _poolCapacity = 10;
//    [SerializeField] private float _spawnRate = 0.5f;
//    [SerializeField] private float _minTimeToDestroy;
//    [SerializeField] private float _maxTimeToDestroy;
//    [SerializeField] private SpawnArea _spawnArea;
    
//    private ObjectPool<Cube> _pool;
//    private Bounds _spawnAreaBounds;

//    private void Awake()
//    {
//       // _spawnAreaBounds = _spawnArea.Collider.bounds;

//        _pool = new ObjectPool<Cube>(
//           createFunc: () => InstantiateCube(),
//           actionOnGet: (cube) => ActivateCube(cube),
//           actionOnRelease: (cube) => cube.gameObject.SetActive(false),
//           actionOnDestroy: (cube) => DestroyCube(cube),
//           defaultCapacity: _poolCapacity);
//    }

//    private void Start()
//    {
//        StartCoroutine(SpawnCube());
//    }

//    private void DestroyCube(Cube cube)
//    {
//        cube.ReachedFloor -= Release;
//        Destroy(cube);
//    }

//    private IEnumerator SpawnCube()
//    {
//        bool isSpawning = true;

//        while (isSpawning)
//        {
//            GetCube();
//            yield return new WaitForSeconds(_spawnRate);
//        }

//    }

//    private Cube InstantiateCube()
//    {
//        Cube cube = Instantiate(_prefab);
//        cube.ReachedFloor += Release;
//        return cube;
//    }

//    private void Release(Cube cube)
//    {
//        _pool.Release(cube);
//    }

//    private void ActivateCube(Cube cube)
//    {
//        cube.transform.position = new Vector3(
//            Random.Range(_spawnAreaBounds.min.x, _spawnAreaBounds.max.x),
//            _spawnArea.transform.position.y, 
//            0);

//        cube.Rigidbody.linearVelocity = Vector3.zero;
//        cube.transform.rotation = Quaternion.identity;
//        cube.UnmarkBumped();
//        cube.gameObject.SetActive(true);
//    }

//    private void GetCube()
//    {
//        _pool.Get();
//    }
//}
