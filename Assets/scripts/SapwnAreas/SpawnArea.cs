using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnArea : MonoBehaviour
{
    public Collider Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider>();       
    }
}
