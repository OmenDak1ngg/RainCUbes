using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private CubeColorChanger _colorChanger;

    public event Action<Cube> ReachedFloor;

    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private float _delay;
    private bool _isBumped;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
        _isBumped = false;
        _delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
    }

    public void MarkBumped()
    {
        _isBumped = true;
    }

    public void UnmarkBumped()
    {
        _isBumped = false;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (_isBumped)
            return;

        if (collider.TryGetComponent<RemovalArea>(out _))
        {
            StartCoroutine(InvokeReachedFloor());
        }
    }

    public IEnumerator InvokeReachedFloor()
    {
        _colorChanger.SetRandomMaterial();
        
        yield return new WaitForSeconds(_delay);
        
        ReachedFloor?.Invoke(this);
        _colorChanger.ResetMaterial();
    }
}
