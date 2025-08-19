using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Spawnpoint[] _spawnPoints;

    public Spawnpoint GetRandomSpawnpoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
    }
}
