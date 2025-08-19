using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explotionForce;

    private float _explotionDelay;

    private float _minExplotionDelay = 2f;
    private float _maxExplotionDelay = 5f;

    private float _currentAlpha;

    private Renderer _renderer;

    public event Action<Bomb> BombExploded;


    private void Start()
    {
        _explotionDelay = UnityEngine.Random.Range(_minExplotionDelay, _maxExplotionDelay);

        _renderer = GetComponent<Renderer>();

        Debug.Log(_explotionDelay);

        _currentAlpha = _renderer.material.color.a;
    }

    public void Detonate()
    {
        StartCoroutine(ExecuteExplotion());
    }

    private IEnumerator ExecuteExplotion()
    {
        Color currentColor = _renderer.material.color;

        while(_currentAlpha > 0)
        {
            currentColor.a = Mathf.MoveTowards(_currentAlpha, 0, Time.deltaTime / _explotionDelay);
            
            yield return null;
        }

        BombExploded?.Invoke(this);
    }
}
