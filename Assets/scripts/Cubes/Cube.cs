using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool IsBumped { get; private set; }
    public event Action<Cube> ReachedFloor;

    private void Awake()
    {
        IsBumped = false;
    }

    public void MarkBumped()
    {
        IsBumped = true;
    }

    public void UnmarkBumped()
    {
        IsBumped = false;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (IsBumped)
            return;

        if(collider.TryGetComponent<RemovalArea>(out _))
        {
            MarkBumped();
            ReachedFloor?.Invoke(this);
        }
    }

}
