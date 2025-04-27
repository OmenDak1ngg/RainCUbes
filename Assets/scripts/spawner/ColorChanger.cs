using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Material _baseMaterial;

    public void SetRandomMaterial(GameObject obj)
    {
        int randomIndex = Random.Range(0, _materials.Length);

        obj.GetComponent<Renderer>().material = _materials[randomIndex];
    }
    
    public void ResetMaterial(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = _baseMaterial;
    }
}
