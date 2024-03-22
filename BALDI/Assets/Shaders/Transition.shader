// This is shader that i wanted to use it on warning screen
// unused cuz i dont know how to apply custom shaders on textmeshpro
// im suck at unity
Shader "Unlit/Transition"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Range ("Range", float) = 0.0
        _iTime ("Elapsed", float) = 0.0
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
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Range;
            float4 _MainTex_ST;
            float _iTime;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float offset = -0.125;
                if (uv.y % 0.125 >= 0.075)
                {
                    offset += uv.y + _iTime;
                } else {
                    offset *= -1.0;
                    offset += -uv.y - _iTime;
                }
                uv.x += offset * _Range;
                fixed4 col = tex2D(_MainTex, uv);
                if (uv.x > 1.0 || uv.y > 1.0 || uv.x < 0.0 || uv.y < 0.0) col.rgb *= 0.0;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
