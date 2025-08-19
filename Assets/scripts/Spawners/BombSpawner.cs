using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    protected override void Start()
    {
        base.Start();
    }

    protected override Bomb OnInstantiateObject()
    {
        Bomb newBomb = base.OnInstantiateObject();

        _cubeSpawner.CubeReleased += GetBomb;
        newBomb.BombExploded += ReleaseObject;

        return newBomb;
    }

    protected override void OnDestroyObject(Bomb poolObject)
    {
        base.OnDestroyObject(poolObject);

        poolObject.BombExploded -= ReleaseObject;
        _cubeSpawner.CubeReleased -= GetBomb;
    }

    private void GetBomb(Vector3 position)
    {
        Bomb GettedBomb = Pool.Get();

        GettedBomb.transform.position = position;
    }

}
