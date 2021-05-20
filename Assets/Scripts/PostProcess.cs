using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    public Material vignetteMaterial;
    public Material defaultMaterial;

    public Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (player.healthBar.hearts == 1) 
            Graphics.Blit(source, destination, vignetteMaterial);
        else
            Graphics.Blit(source, destination, defaultMaterial);
    }
}
