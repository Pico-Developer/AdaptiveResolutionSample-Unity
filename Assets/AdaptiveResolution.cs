
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

#if UNITY_ANDROID
using Unity.XR.PXR;
#endif

public class AdaptiveResolution : MonoBehaviour
{
	public TextMeshProUGUI textMesh;
	public TextMeshProUGUI textMesh2;
	public TextMeshProUGUI textMesh3;

	private void Update()
	{
		float newRenderScale = XRSettings.renderViewportScale;
		
		Debug.Log(" RenderViewportScale: " + newRenderScale);

		int w = (int)((float)XRSettings.eyeTextureWidth * newRenderScale);
		int h = (int)((float)XRSettings.eyeTextureHeight * newRenderScale);
		string resString = w + " X " + h + "  S: " + newRenderScale.ToString("0.00");
		textMesh.text = resString;
		textMesh2.text = resString;
		textMesh3.text = resString;
		Debug.Log(" Resolution: " + resString);
	}
}

