// 7/7/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

Shader "Custom/RotateTexture"
{
    Properties
    {
        _BaseMap ("Base Map", 2D) = "white" {}
        _Rotation ("Rotation (Degrees)", Range(0, 360)) = 0
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" }
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            float _Rotation;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS);

                // Rotate UVs
                float rad = radians(_Rotation);
                float2x2 rotationMatrix = float2x2(cos(rad), -sin(rad), sin(rad), cos(rad));
                o.uv = mul(rotationMatrix, v.uv - 0.5) + 0.5;

                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                return SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);
            }
            ENDHLSL
        }
    }
}