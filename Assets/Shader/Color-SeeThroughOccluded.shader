Shader "Unlit/Color-SeeThroughOccluded"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1)
    }
 
        SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        Color[_Color]
        Pass
        {
            Tags { "Queue" = "Geometry+1" }
            ZTest Greater
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
        }
    }
}