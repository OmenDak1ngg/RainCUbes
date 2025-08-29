using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class ExplodableObject : MonoBehaviour
{
    protected ColorChanger ColorChanger;

    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    protected virtual void Awake()
    {
        Renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void SetColorChanger(ColorChanger colorChanger)
    {
        ColorChanger = colorChanger;
    }
}
