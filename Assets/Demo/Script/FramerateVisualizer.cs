using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.PXR;
using UnityEngine;

public class FramerateVisualizar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private int fps;

    void Update()
    {
        PXR_Plugin.Pxr_GetConfigInt(ConfigType.RenderFPS, ref fps);
        textMesh.text = $"{fps} FPS";
    }

}