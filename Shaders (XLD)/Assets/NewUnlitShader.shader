// float = float
// float2 = vector2
// float3 == vector3
// float4 = vector4

// 16 bit, floating point
// half  = half a float
// half2, half3, half4

// 12 bit, fixed point
// fixed
// fixed = -2 to 2
// fixed2, fixed3, fixed4

// Colors




// float3x3


//ShaderLab
Shader "Unlit/NewUnlitShader"
{
    //Properties displayed in the Unity Inspector
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Offset ("Offset", float) = 1
        _Scale ("Scale", float) = 1
        _ColourStart ("Colour Start", Color) = (0,0,0,1)
        _ColourEnd ("Colour End", Color)   = (1,1,1,1)
    }
    SubShader
    {
        // What type of shader, opaque in this case
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            //unity shader help functions
            #include "UnityCG.cginc"


            #define TAU 6.283185307179586
            #define HALF_TAU TAU * 0.5
            #define TAUDIRTY UNITY_TWO_PI
            
            struct appdata // mesh data
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                // X Y Z ( vector3)
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Offset;
            float _Scale;
            float4 _ColourStart;
            float4 _ColourEnd;

            // Running once per vertex per frame
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal( v.normal);
                o.uv = (v.uv + _Offset + _Time.y ) * _Scale;
                return o;
                
            }

            // Running once per texel ("pixels") per frame
            fixed4 frag (v2f i) : SV_Target
            {
                float xOffset = cos(i.uv.y * TAU * 8) * 0.01;
                float t = cos((i.uv.x + xOffset) * TAU * 10) * 0.5 + 0.5;
                // t = 0.1     is the percentage between both a and b                 //t = 0 to 1
                // a = 10        b = 20          == 11 being the middle ground because it's 1% of the way


                float4 col = lerp(_ColourStart, _ColourEnd, t);
                
                //fixed4 col = fixed4(i.uv, 0, 0);
                //fixed4 col = fixed4(i.normal.xyz, 0);
                //fixed4 col = fixed4(i.uv.xxx, 1);
                return col;
            }
            ENDCG
        }
    }
}
