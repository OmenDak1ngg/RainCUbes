using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool IsBumped { get; private set; }    

    private void Awake()
    {
        IsBumped = false;
    }

    public void MarkReleased()
    {
        IsBumped = true;
    }

    public void UnmarkReleased()
    {
        IsBumped = false;
    }
}
