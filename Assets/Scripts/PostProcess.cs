using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    public Material vignetteMaterial;
    public Material defaultMaterial;

    public PlayerStats stats;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (stats.healthBar.hearts == 1) 
            Graphics.Blit(source, destination, vignetteMaterial);
        else
            Graphics.Blit(source, destination, defaultMaterial);
    }
}
