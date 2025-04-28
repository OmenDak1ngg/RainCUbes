using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool IsReleased { get; private set; }    

    private void Awake()
    {
        IsReleased = false;
    }

    public void MarkReleased()
    {
        IsReleased = true;
    }

    public void UnmarkReleased()
    {
        IsReleased = false;
    }
}
