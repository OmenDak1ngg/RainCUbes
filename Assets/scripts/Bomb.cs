using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explotionForce;
    [SerializeField] private float _explotionRadius;

    private float _explotionDelay;

    private float _minExplotionDelay = 2f;
    private float _maxExplotionDelay = 5f;

    private Renderer _renderer;

    public event Action<Bomb> BombExploded;

    private void Awake()
    {
        _explotionDelay = UnityEngine.Random.Range(_minExplotionDelay, _maxExplotionDelay);

        _renderer = GetComponent<Renderer>();
    }

    public void Detonate()
    {
        StartCoroutine(ExecuteExplotion());
    }

    private IEnumerator ExecuteExplotion()
    {
        Color currentColor = _renderer.material.color;

        while(currentColor.a > 0)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, 0, Time.deltaTime / _explotionDelay);
            _renderer.material.color = currentColor;

            yield return null;
        }

        foreach(Cube cube in FindExplodableObjects())
        {
            cube.Rigidbody.AddExplosionForce(_explotionForce, transform.position, _explotionRadius, 0f , ForceMode.Impulse);
        }

        BombExploded?.Invoke(this);
    }

    private List<Cube> FindExplodableObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explotionRadius);

        List<Cube> cubes = new List<Cube>();

        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent<Cube>(out Cube cube) == false)
                continue;

            cubes.Add(cube);
        }

        return cubes;
    }
}
