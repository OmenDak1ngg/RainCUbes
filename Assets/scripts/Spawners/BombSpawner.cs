using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeReleased += GetBomb;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeReleased -= GetBomb;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void GetBomb(Vector3 position)
    {

        Bomb gettedBomb = Pool.Get();

        gettedBomb.Explode();

        gettedBomb.transform.position = position;
    }

    protected override Bomb OnInstantiateObject()
    {
        Bomb newBomb = base.OnInstantiateObject();

        newBomb.BombExploded += ReleaseObject;

        return newBomb;
    }

    protected override void OnDestroyObject(Bomb poolObject)
    {
        base.OnDestroyObject(poolObject);

        poolObject.BombExploded -= ReleaseObject;
    }

    protected override void ReleaseObject(Bomb bomb)
    {
        base.ReleaseObject(bomb);
    }
}
