using System.Collections;
using UnityEngine;

public class DmgFlash_SteampunkColossus : MonoBehaviour
{
    [SerializeField] private float flashTime = .15f;
    [SerializeField] private MeshRenderer[] meshRenderer;

    private Color[] _origColor; 

    private void Start()
    {
        _origColor = new Color[meshRenderer.Length];

        for (int i = 0; i < meshRenderer.Length; i++)
        {
            _origColor[i] = meshRenderer[i].material.color;
        }
        
    }

    public void Flash() => StartCoroutine(FlashEffect());

    private IEnumerator FlashEffect()
    {
        for (int i = 0;i < meshRenderer.Length; i++)
        {
            Material[] materials = meshRenderer[i].materials;
            foreach (Material material in materials)
            {
                material.color = Color.white;
            }

            meshRenderer[i].materials = materials;
        }
        

        
        yield return new WaitForSeconds(flashTime);

        for (int i = 0; i < meshRenderer.Length; i++)
        {
            Material[] materials = meshRenderer[i].materials;
            foreach (Material material in materials)
            {
                material.color = _origColor[i];
            }

            meshRenderer[i].materials = materials;
        }
    }
}
