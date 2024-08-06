Shader "Unlit/HealthBarShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Fear ("Fear", Range(0.0, 50.0)) = 25
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Fear;

            float GetWave (float2 uv)
            {
                float2 uvsCenetered = uv * 2 -1;
                float radialDistance = length( uvsCenetered);
                float wave = cos((radialDistance - _Time.y * 0.1) * 5) * 0.5 + 0.5;
                return wave;
            }
            
            v2f vert (appdata v)
            {
                v2f o;
                //v.vertex.y = GetWave(v.uv);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex.x = round(o.vertex.x * _Fear) / _Fear;
                o.vertex.y = round(o.vertex.y * _Fear) / _Fear;

                o.vertex.x = cos(o.vertex.x - _Time.y * 0.01) ;
                
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return GetWave(i.uv).xxxx;
            }
            ENDCG
        }
    }
}
