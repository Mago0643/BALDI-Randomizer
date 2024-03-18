// Shader Made By Lego0_77
// You can use it by adding credit list to me.
// (or code)

Shader "Unlit/Rainbow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _iTime ("Elapsed Time", float) = 0.0
        _Dark ("Dark", Range(0.0,1.0)) = 1.0
        _Bright ("Bright", Range(0.0,1.0)) = 1.0
        _Speed ("Rainbow Speed", Range(0.5, 10.0)) = 1.0
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
            float _Dark;
            float _Bright;
            float _Speed;

            fixed4 mix(fixed4 x, fixed4 y, fixed4 a)
            {
                return x*(1.0-a)+y*a;
            }
            fixed3 mix(fixed3 x, fixed3 y, fixed3 a)
            {
                return x*(1.0-a)+y*a;
            }

            fixed3 fract(fixed3 x)
            {
                return x - floor(x);
            }

            // https://stackoverflow.com/questions/15095909/from-rgb-to-hsv-in-opengl-glsl
            fixed3 rgb2hsv(fixed3 c)
            {
                fixed4 K = fixed4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                fixed4 p = mix(fixed4(c.bg, K.wz), fixed4(c.gb, K.xy), step(c.b, c.g));
                fixed4 q = mix(fixed4(p.xyw, c.r), fixed4(c.r, p.yzx), step(p.x, c.r));

                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return fixed3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            fixed3 hsv2rgb(fixed3 c)
            {
                fixed4 K = fixed4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                fixed3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
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
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // repeats 0 --> 360
                float hue = (_iTime*_Speed+(i.uv.y-i.uv.x)) % 360.0;
                // convert hsv to rgb
                fixed3 hueToColor = hsv2rgb(fixed3(hue, _Bright, _Dark));
                // apply to image (with alpha)
                col.rgb *= hueToColor * col.a;

                // make alpha transparent
                clip(col.a - 0.5);

                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
