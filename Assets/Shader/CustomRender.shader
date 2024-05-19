Shader "Custom/CustomRender"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _OccludedColor("Occluded Color", Color) = (1,1,1,0.2)
        _LineColor("Line Color", Color) = (0, 0, 0, 1) // Цвет линий
        _LineWidth("Line Width", Float) = 0.01 // Ширина линий
    }

    SubShader {

        Pass {
            Tags { "LightMode" = "UniversalForward" }
            LOD 200
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma instancing_options assumeuniformscaling
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5

            float4 _Color;

            struct VertexIn
            {
                float4 vertex : POSITION;
            };

            struct VertexOut
            {
                float4 color : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            VertexOut vert(VertexIn vIn)
            {
                VertexOut vOut;
                vOut.color = _Color;
                vOut.vertex = UnityObjectToClipPos(vIn.vertex);
                return vOut;
            }

            fixed4 frag(VertexOut vIn) : SV_Target
            {
                return vIn.color;
            }
            ENDCG
        }

        Pass 
        { 
            Tags { "LightMode" = "SRPDefaultUnlit" }
            ZTest Greater
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma instancing_options assumeuniformscaling
            #pragma vertex vert          
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            half4 _OccludedColor;

            float4 vert(float4 pos : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(pos);
            }

            half4 frag(float4 pos : SV_POSITION) : COLOR
            {
                _OccludedColor.a = 0.2;
                return _OccludedColor;
            }
            ENDCG
        }

        // Pass для отрисовки граней
        Pass {
            Tags { "LightMode" = "Always" }
            Cull Off
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vertLine
            #pragma fragment fragLine

            float4 _LineColor;
            float _LineWidth;

            struct VertexIn {
                float4 vertex : POSITION;
            };

            struct VertexOut {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            VertexOut vertLine(VertexIn vIn) {
                VertexOut vOut;
                vOut.pos = UnityObjectToClipPos(vIn.vertex);
                vOut.screenPos = ComputeScreenPos(vOut.pos);
                return vOut;
            }

            fixed4 fragLine(VertexOut vOut) : SV_Target {
                float edge = _LineWidth / length(ddx(vOut.screenPos.xy) + ddy(vOut.screenPos.xy));
                return lerp(_LineColor, float4(0, 0, 0, 0), edge);
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
