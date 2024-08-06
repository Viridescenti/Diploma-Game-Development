Shader "Unlit/TransparentShader"
{
    Properties
    {
       _ColorA("Color A", Color) = (1, 1, 1, 1)
       _ColorB("Color B", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent"
                "Queue" = "Transparent"} 
        /* 
         * Opaque is DEFAULT
         * Tags { "Queue" = "Background" } goes from 0 to 1499, default value 1000.
         * Tags { "Queue" = "Geometry" } goes from 1500 to 2399, default value 2000
         * Tags { "Queue" = "AlphaTest" } goes from 2700 to 3599, default value 3000 
        == is used on semi-transparent objects (eg. glass, grass or vegetation)
         * Tags { "Queue" = "Overlay" } goes from 3600 to 5000, default value 4000
        */

        
            Pass
        {
            // Z-Buffer / Depth-Buffer options
            Zwrite Off
            ZTest Greater // Disabled, Never, Less, Equal, LEqual, Greater,  NotEqual, GEqual, Always
            Cull Front // Front, Back, Off

            
            // Blending Options
            Blend OneMinusDstColor One
            
            /*
            * Blend Off - is the DEFAULT
            * Blend One One //Additive
            * Blend OneMinusDstColor One // Soft Additive
            * Blend SrcAlpha OneMinusSrcAlpha // Traditional Transparency  - multiplying colour by the transparency
            * Blend OneMinusSrcAlpha // Premultiplied transparency
            * Blend DstColor Zero // Multiplicative
            * Blend DstColor SrcColor // 2x Multiplicative
            
            * Blend SrcColor One // Blending overlay
            * Blend OneMinusSrcColor One // Soft light blending
            * Blend Zero OneMinusSrcColor // Negative color blending
            */
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _ColorA;
            float4 _ColorB;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = lerp (_ColorA, _ColorB, i.uv.y);
                return col;
            }
            ENDCG
        }
    }
}
