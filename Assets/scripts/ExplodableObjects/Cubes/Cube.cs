using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : ExplodableObject
{
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    

    private float _delay;
    private bool _isBumped;

    public event Action<Cube> ReachedFloor;

    protected override void Awake()
    {
        base.Awake();

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
         
            _isBumped = true;
        }
    }

    public IEnumerator InvokeReachedFloor()
    {
        if (_isBumped)
            yield break;

        ColorChanger.SetRandomMaterial(this.Renderer);
        
        yield return new WaitForSeconds(_delay);
        
        ReachedFloor?.Invoke(this);
        ColorChanger.ResetMaterial(this.Renderer);
    }
}
