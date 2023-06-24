Shader "Custom/GlitchBackground"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Range(0.1, 10)) = 1
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float _Speed;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 uv = i.uv.xy;
                float red = tex2D(_MainTex, uv + 0.01 * sin(_Time.y * _Speed)).r;
                float green = tex2D(_MainTex, uv + 0.02 * sin(_Time.y * _Speed)).g;
                float blue = tex2D(_MainTex, uv - 0.01 * sin(_Time.y * _Speed)).b;
                fixed4 color = fixed4(red, green, blue, 1);
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
