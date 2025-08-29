using System;
using System.Collections;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public event Action BombDetonated;

    public void DetonateBomb()
    {
        BombDetonated?.Invoke();
    }
}
