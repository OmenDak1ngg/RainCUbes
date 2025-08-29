using System.Collections.Generic;
using UnityEngine;

public class ExplodableObjectsFounder : MonoBehaviour
{
    public List<Collider> FoundExplodableObjects(float searchRadius, Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, searchRadius);

        List<Collider> explodableObjects = new List<Collider>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<ExplodableObject>(out _))
                explodableObjects.Add(collider);
        }

        return explodableObjects;
    }
}
