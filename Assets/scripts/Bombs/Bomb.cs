using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Detonator _detonator;

    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;

    public float ExplotionDelay { get; private set; }

    private float _minExplotionDelay = 2f;
    private float _maxExplotionDelay = 5f;

    public Renderer Renderer { get; private set; }  

    public event Action<Bomb> BombExploded;

    private void Awake()
    {
        ExplotionDelay = UnityEngine.Random.Range(_minExplotionDelay, _maxExplotionDelay);

        Renderer = GetComponent<Renderer>();
    }

    private List<Collider> FindExplodableObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explotionRadius);

        List<Collider> explodableObjects = new List<Collider>();

        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent<Cube>(out _) || collider.TryGetComponent<Bomb>(out _))
                explodableObjects.Add(collider);
        }

        return explodableObjects;
    }

    public void Explode()
    {
        foreach (Collider collider in FindExplodableObjects())
        {
            collider.GetComponent<Rigidbody>().AddExplosionForce(_explotionForce, transform.position, _explotionRadius, 0f, ForceMode.Impulse);
        }

        BombExploded?.Invoke(this);
    }

    public void ExecuteDetonation()
    {
        Color currentColor = Renderer.material.color;

        _detonator.DetonateBomb();
    }
}
