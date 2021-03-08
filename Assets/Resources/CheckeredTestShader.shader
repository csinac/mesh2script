Shader "RT/Mesh2Script/CheckeredTestShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = (v.normal / 2 + 0.5);
                return o;
            }

            bool CheckerDark(float2 uv) {
                int x = uv.r * 50;
                int y = uv.g * 50;

                return x % 2 == 1 && y % 2 == 1;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col;
                col.rgb = i.normal;

                if(CheckerDark(i.uv)) {
                    col.rgb *= 0.95;
                }

                return col;
            }

            ENDCG
        }
    }
}
