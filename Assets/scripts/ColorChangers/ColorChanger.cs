using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material[] _Cubematerials;
    [SerializeField] private Material _baseCubeMaterial;

    private IEnumerator ExecuteChangingBombAlphaToZero(Bomb bomb)
    {
        Color currentColor = bomb.Renderer.material.color;

        while (currentColor.a > 0)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, 0, Time.deltaTime / bomb.ExplotionDelay);
            bomb.Renderer.material.color = currentColor;

            yield return null;
        }
    }

    public void SetRandomMaterial(Renderer renderer)
    {
        int randomIndex = Random.Range(0, _Cubematerials.Length);

        renderer.material = _Cubematerials[randomIndex];
    }

    public void ResetMaterial(Renderer renderer)
    {
        renderer.material = _baseCubeMaterial;
    }

    public void ChangeAlphaToZero(Bomb bomb)
    {
        StartCoroutine(ExecuteChangingBombAlphaToZero(bomb));
    }

    public void ResetAlpha(Renderer renderer)
    {
        Color currentColor = renderer.material.color;

        currentColor.a = 1f;

        renderer.material.color = currentColor;
    }
}
