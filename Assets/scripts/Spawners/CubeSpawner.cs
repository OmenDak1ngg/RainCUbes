using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _spawnDelay = 1f;

    [SerializeField] private SpawnArea _spawnArea;

    private WaitForSeconds _spawnDelayYield;

    public event Action<Vector3> CubeReleased;

    protected override void Start()
    {
        base.Start();

        _spawnDelayYield = new WaitForSeconds(_spawnDelay);

        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (enabled) 
        {
            Pool.Get();

            yield return _spawnDelayYield;
        }
    }

    protected override Cube OnInstantiateObject()
    {
        Cube newCube = base.OnInstantiateObject();

        newCube.ReachedFloor += ReleaseObject;

        return newCube;
    }

    protected override void OnObjectGet(Cube poolObject)
    {
        base.OnObjectGet(poolObject);

        poolObject.transform.position = _spawnArea.GetRandomSpawnpoint().transform.position;
    }

    protected override void OnObjectRelease(Cube poolObject)
    {
        poolObject.UnmarkBumped();

        CubeReleased?.Invoke(poolObject.transform.position);

        base.OnObjectRelease(poolObject);
    }

    protected override void OnDestroyObject(Cube poolObject)
    {
        poolObject.ReachedFloor -= ReleaseObject;

        base.OnDestroyObject(poolObject);
    }
}
