using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Material _baseMaterial;
    [SerializeField] private Cube cube;

    public void SetRandomMaterial()
    {
        int randomIndex = Random.Range(0, _materials.Length);

        cube.Renderer.material = _materials[randomIndex];
    }
    
    public void ResetMaterial()
    {
        cube.Renderer.material = _baseMaterial;
    }
}
