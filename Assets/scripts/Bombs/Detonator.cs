using System.Collections;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    [SerializeField] private BombAlphaChanger _alphaChanger;

    private WaitForSeconds _explotionDelay;

    private Bomb _bomb;

    private void Awake()
    {
        _bomb = transform.parent.GetComponent<Bomb>();
        if (_bomb == null) Debug.Log("NOBIBMBA");
        Debug.Log(_bomb.name);
        _explotionDelay = new WaitForSeconds(_bomb.ExplotionDelay);
    }

    public void DetonateBomb()
    {
        StartCoroutine(ExecuteBombDeotonation());
    }

    private IEnumerator ExecuteBombDeotonation()
    {
        _alphaChanger.ChangeAlphaToZero(_bomb);

        yield return _explotionDelay;

        _bomb.Explode();

        _alphaChanger.ResetAlpha(_bomb);
    }
}
