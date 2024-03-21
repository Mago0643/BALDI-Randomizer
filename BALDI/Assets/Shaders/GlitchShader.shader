// Made By Lego0_77
// You can use this if you credit me
Shader "Unlit/GlitchShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Amp ("Amp", float) = 0.0
        _GlitchOffset ("Glitch Offset", float) = 0.0
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
            float4 _MainTex_ST;
            float _Amp;
            float _GlitchOffset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float texStuff(fixed2 uv, int number, float offset)
            {
                float offsetREAL = 0.0;
                if (floor(uv.y * 10.0 % 2.0) == 0.0)
                {
                    if (floor(uv.y * 10.0 % 4.0) == 0.0)
                        offsetREAL = _GlitchOffset;
                    else
                        offsetREAL = -_GlitchOffset;
                }
                return tex2D(_MainTex, fixed2((uv.x + offset) + offsetREAL, uv.y))[number];
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                col.r = texStuff(i.uv, 0, -_Amp);
                col.g = texStuff(i.uv, 1, 0.0);
                col.b = texStuff(i.uv, 2, _Amp);
                col.a = texStuff(i.uv, 3, 0.0);

                UNITY_APPLY_FOG(i.fogCoord, col);
                clip(col.a - 0.5);
                return col;
            }
            ENDCG
        }
    }
}
