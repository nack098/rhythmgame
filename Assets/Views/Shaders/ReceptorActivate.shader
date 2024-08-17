Shader "Unlit/ReceptorActivate"
{
    Properties
    {
        _TopColor("Top Gradient Colour", Color) = (1,1,1,1)
        _BottomColor("Bottom Gradient Colour", Color) = (0,0,0,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                UNITY_FOG_COORDS(1)
                float4 color: TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _TopColor;
            fixed4 _BottomColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = lerp(_BottomColor, _TopColor, v.uv.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                return i.color;
            }
            ENDCG
        }
    }
}
