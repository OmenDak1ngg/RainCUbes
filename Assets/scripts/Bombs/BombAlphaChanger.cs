using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BombAlphaChanger : MonoBehaviour
{
    public void ChangeAlphaToZero(Bomb bomb)
    {
        StartCoroutine(ExecuteChangingAlphaToZero(bomb));
    }

    private IEnumerator ExecuteChangingAlphaToZero(Bomb bomb)
    {
        Color currentColor = bomb.Renderer.material.color;

        while (currentColor.a > 0)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, 0, Time.deltaTime / bomb.ExplotionDelay);
            bomb.Renderer.material.color = currentColor;

            yield return null;
        }
    }

    public void ResetAlpha(Bomb bomb)
    {
        Color currentColor = bomb.Renderer.material.color;

        currentColor.a = 1f;

        bomb.Renderer.material.color = currentColor;
    }
}
