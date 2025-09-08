using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material[] _allMaterials;
    [SerializeField] private Material _baseMaterial;

    public IEnumerator ExecuteChangingAlphaToZero(Renderer renederer, float changingDelay)
    {
        Color currentColor = renederer.material.color;

        while (currentColor.a > 0)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, 0, Time.deltaTime / changingDelay);
            renederer.material.color = currentColor;

            yield return null;
        }
    }

    public void SetRandomMaterial(Renderer renderer)
    {
        int randomIndex = Random.Range(0, _allMaterials.Length);

        renderer.material = _allMaterials[randomIndex];
    }

    public void ResetMaterial(Renderer renderer)
    {
        renderer.material = _baseMaterial;
    }

    public void ResetAlpha(Renderer renderer)
    {
        Color currentColor = renderer.material.color;

        currentColor.a = 1f;

        renderer.material.color = currentColor;
    }
}
