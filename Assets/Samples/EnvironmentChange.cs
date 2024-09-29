using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnvironmentChange : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] m_LightTex;
    [SerializeField]
    private Texture2D[] mLightDir;
    [SerializeField]
    private Texture2D[] mLightShadowMask;

    [SerializeField]
    private Texture2D[] m_DarkTex;
    [SerializeField]
    private Texture2D[] mDarkDir;
    [SerializeField]
    private Texture2D[] mDarkShadowMask;

    [SerializeField]
    private Material mLightMat;
    [SerializeField]
    private Material mDarkMat;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeLightMap", 0f, 60f);
    }


    private bool IsLight()
    {
        Debug.Log("DateTime.Now.Hour: " + DateTime.Now.Hour);
        if (DateTime.Now.Hour >= 7 && DateTime.Now.Hour < 18)
            return true;
        return false;
    }


    private void ChangeLightMap()
    {
        Texture2D[] texTemp = IsLight() ? m_LightTex : m_DarkTex;
        Texture2D[] texDir = IsLight() ? mLightDir : mDarkDir;
        Texture2D[] texShadow = IsLight() ? mLightShadowMask : mDarkShadowMask;
        Material sky = IsLight() ? mLightMat : mDarkMat;
        LightmapData[] lightmapArray = LightmapSettings.lightmaps;
        for (int i = 0; i < lightmapArray.Length; i++)
        {
            lightmapArray[i] = new LightmapData();
            lightmapArray[i].lightmapColor = texTemp[i];
            if (texDir != null && texDir.Length > i && texDir[i] != null)
                lightmapArray[i].lightmapDir = texDir[i];
            if (texShadow != null && texShadow.Length > i && texShadow[i] != null)
                lightmapArray[i].shadowMask = texShadow[i];
        }
        if(sky != null)
            RenderSettings.skybox = sky;
        LightmapSettings.lightmaps = lightmapArray;
    }


    private void OnApplicationPause(bool pause)
    {
        if (!pause)
            ChangeLightMap();
    }

}
