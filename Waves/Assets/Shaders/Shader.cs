using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Shader : MonoBehaviour
{
    public Material effectMaterial;
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source,destination,effectMaterial);
    }
}
