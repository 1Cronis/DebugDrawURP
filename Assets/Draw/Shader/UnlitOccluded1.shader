Shader "Custom/UnlitOccluded1"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _OccludedColor("Occluded Color", Color) = (1,1,1,0.2)
    }

    SubShader {

       Pass {
            Tags { "LightMode" = "UniversalForward" }
            //Tags { "RenderType"="Opaque" "Queue"="Geometry+1"} // ��� ������� - ������������ ������, ����������� ������� ���������� ��� Geometry
            LOD 200                          // ������� ����������� (Level of Detail)
            ZWrite On                        // ��������� ������ � ����� �������
            ZTest LEqual                     // ���� �������, ��������, ���� ������� �������� ������� ������ ��� ����� �������� � ������ �������
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
       

            //Tags { "Queue"="Geometry+1" } // ������� ����������, ����������� �� 1 ��� Geometry
            ZTest Greater                 // ���� �������, ��������, ���� ������� �������� ������� ������, ��� �������� � ������ �������
            ZWrite Off                   // ���������� ������ � ����� �������
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma instancing_options assumeuniformscaling
            #pragma vertex vert          
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            half4 _OccludedColor; // ���� ��� �������, ����� ������ ��������� � ����

            float4 vert(float4 pos : POSITION) : SV_POSITION
            {
                float4 viewPos = UnityObjectToClipPos(pos);
                return viewPos;
            }

            half4 frag(float4 pos : SV_POSITION) : COLOR
            {
                _OccludedColor.a = 0.2;
                return _OccludedColor;
            }
 
            ENDCG
        }

        
    }

    FallBack "Diffuse" // �������� ������� - ������������ Diffuse shader, ���� ������� �� ��������������
}
