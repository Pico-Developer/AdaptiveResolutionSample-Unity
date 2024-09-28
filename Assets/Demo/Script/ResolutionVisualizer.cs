using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System.Threading;

#if UNITY_ANDROID
using Unity.XR.PXR;
#endif
public class ResolutionVisualizer : MonoBehaviour
{
     [SerializeField] private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh= this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float newRenderScale = XRSettings.renderViewportScale;
        Debug.Log(" RenderViewportScale: " + newRenderScale);

        int w = (int)((float)XRSettings.eyeTextureWidth * newRenderScale);
        int h = (int)((float)XRSettings.eyeTextureHeight * newRenderScale);

        string resString = w + " X " + h;
        textMesh.text = resString;


        Debug.Log(" Resolution: " + resString);
    }
}
