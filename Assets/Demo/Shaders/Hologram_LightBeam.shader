Shader "Custom/Hologram_LightBeam"
{
    Properties
    {
        [NoScaleOffset]_LightBeamTex("LightBeamTex", 2D) = "white" {}
        [HDR]_BaseColor ("BaseColor", Color) = (1,1,1,0)
        _RandomBounds("EmissionMultiplierBounds", Vector) = (0.7, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
               "BuiltInMaterialType" = "Lit"
                "Queue"="Transparent"}

Cull Off

Blend SrcAlpha One, One One
                ZTest LEqual
                ZWrite Off
ColorMask RGB
        CGPROGRAM
        #include "UnityCG.cginc"
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard keepalpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

struct Input
{
    float2 uv_LightBeamTex;

};

sampler2D _LightBeamTex;
float4 _BaseColor;
float4 _RandomBounds;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)


// copied from shadergraph node
void random_range(float2 Seed, float Min, float Max, out float Out)
{
    float randomno =  frac(sin(dot(Seed, float2(12.9898, 78.233)))*43758.5453);
    Out = lerp(Min, Max, randomno);
}

void surf(Input IN, inout SurfaceOutputStandard o)
{
    float4 mainColor = tex2D(_LightBeamTex, IN.uv_LightBeamTex);
    float3 baseColor = IsGammaSpace()? LinearToGammaSpace(_BaseColor.rgb) : _BaseColor.rgb;
    float eScale;
    random_range(_Time.yy, _RandomBounds.x, _RandomBounds.y, eScale);
    o.Albedo = mainColor.rgb * baseColor;
    o.Emission = o.Albedo * eScale;
    o.Alpha = mainColor;
}
        ENDCG
    }
FallBack"Diffuse"
}
