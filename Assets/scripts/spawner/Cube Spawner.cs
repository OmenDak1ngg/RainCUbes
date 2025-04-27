using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;
    [SerializeField] private float _spawnRate = 0.5f;

    private ObjectPool<GameObject> _pool;
    //createFunc - что нужно сделать при создании нового обьекта
    //actiononget - действие при взятии свободного обьекта из пула
    // actiononrelease - что сделать при возвращении обьекта в пул 
    //actionondestroy - действие при удалении обьекта из пула
    //collectioncheck - необходимо ли проверять колекциб при возвращении обьекта в пул (только в редакторе_)
    //defaultcapacity - стандартное значение пула
    //maxsize - max размер пула

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _spawnRate);
    }

    private void ActionOnGet(GameObject obj)
    {
        Collider SpawnAreaCollider =  _spawnArea.GetComponent<Collider>();
        Bounds bounds = SpawnAreaCollider.bounds;

        
        obj.transform.position = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x), _spawnArea.transform.position.y  ,0);

        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        obj.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    public IEnumerator ReleaseWithDelay(GameObject obj, float delay, ColorChanger _colorChanger)
    {
        yield return new WaitForSeconds(delay);
        _colorChanger.ResetMaterial(obj);
        _pool.Release(obj);
    }
}
