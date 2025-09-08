using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Bomb : ExplodableObject
{

    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;
    [SerializeField] private Detonator _detonator;

    private ExplodableObjectsFounder _explodedObjectsFounder;
    
    private float _minExplotionDelay = 2f;
    private float _maxExplotionDelay = 5f;

    public Detonator Detonator => _detonator;
    public float ExplotionDelay { get; private set; }

    public event Action<Bomb> BombExploded;

    private void OnEnable()
    {
        _detonator.BombDetonated += ExecuteExplotion;
    }

    private void OnDisable()
    {
        _detonator.BombDetonated -= ExecuteExplotion;
    }

    protected override void Awake()
    {
        base.Awake();

        ExplotionDelay = UnityEngine.Random.Range(_minExplotionDelay, _maxExplotionDelay);
    }

    private IEnumerator Explode()
    {
        yield return ColorChanger.ExecuteChangingAlphaToZero(Renderer, ExplotionDelay);

        foreach (Collider collider in _explodedObjectsFounder.FoundExplodableObjects(_explotionRadius, transform.position))
        {
            collider.GetComponent<Rigidbody>().AddExplosionForce(_explotionForce, transform.position, _explotionRadius, 0f, ForceMode.Impulse);
        }

        BombExploded?.Invoke(this);
    }

    public void ExecuteExplotion()
    {
        StartCoroutine(Explode());
    }

    public void SetExplodableObjectsFounder(ExplodableObjectsFounder explodableObjectsFounder)
    {
        _explodedObjectsFounder = explodableObjectsFounder;
    }
}
