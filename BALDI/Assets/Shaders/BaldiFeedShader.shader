Shader "Custom/BaldiFeed"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _iTime ("Elapsed", float) = 0.0
        _Circle ("Circle Amount", float) = 0.0
        _Speed ("Speed", float) = 1.0
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
            float _iTime;
            float _Circle;
            float _Speed;

            fixed2 fract(fixed2 x)
            {
                return x - floor(x);
            }

            float random (fixed2 st) {
                return fract(sin(dot(st.xy,fixed2(12.9898,78.233)))*43758.5453123);
            }

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
                if (_Circle != 0)
                {
                    uv.x += sin(_iTime * _Speed) * _Circle;
                    uv.y += cos(_iTime * _Speed) * _Circle;
                }

                fixed4 col = tex2D(_MainTex, uv);
                clip(col.a - 0.5);
                return col;
            }
            ENDCG
        }
    }
}
