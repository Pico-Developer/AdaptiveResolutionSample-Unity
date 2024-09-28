Shader"Custom/Hologram_Animated"
{
    Properties
    {
        _HologramTex("HologramTexture", 2D) = "white" {}
        _BaseColor ("BaseColor", Color) = (1,1,1,0)
        [HDR]_FresnelColor("FresnelColor", Color) = (1, 1, 1, 1)
        _FresnelPower("FresnelPower", Range(1, 5)) = 3
        _RandomBounds("EmissionMultiplierBounds", Vector) = (0.25, 0.5, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
               "BuiltInMaterialType" = "Lit"
                "Queue"="Transparent"}

Cull Back

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
    float3 viewDir;
    float3 worldNormal;
    float4 screenPos;

};

sampler2D _HologramTex;
float4 _HologramTex_ST;

float4 _BaseColor;
float4 _FresnelColor;
float _FresnelPower;
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
    float2 offset = _HologramTex_ST.zw * _Time.yy;
    float2 transformedUV = IN.screenPos.xy / IN.screenPos.w * _HologramTex_ST.xy + offset;
    float4 scanlineColor = tex2D(_HologramTex, transformedUV);
    
    float3 fresnelColor = IsGammaSpace() ? LinearToGammaSpace(_FresnelColor.rgb) : _FresnelColor.rgb;
    float strength = pow((1.0 - saturate(dot(normalize(IN.worldNormal), normalize(IN.viewDir)))), _FresnelPower);

    float randomStrength;
    random_range(_Time.yy, _RandomBounds.x, _RandomBounds.y, randomStrength);
    o.Albedo = (IsGammaSpace() ? _BaseColor.rgb : GammaToLinearSpace(_BaseColor.rgb)) * scanlineColor.rgb;
    o.Emission = (strength * fresnelColor + (1 - scanlineColor) * _BaseColor) * randomStrength;
    o.Alpha = scanlineColor;
}
        ENDCG
    }
FallBack"Diffuse"
}
