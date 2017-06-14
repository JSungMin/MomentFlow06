Shader "Custom/GradationShader"
{
	Properties {
        _Color ("Color", Color) = (0,0,0,0)
    }
    SubShader {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent"}
        LOD 200
        Lighting Off

        GrabPass { }

        CGPROGRAM
        #include "Assets/Shader/BlendMode.cginc"
        #pragma surface surf Lambert

        sampler2D _GrabTexture;
        half4  _Color;

        struct Input {
            float4 screenPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 grabTexcoord = IN.screenPos.xy / IN.screenPos.w;

            float4 c = tex2D(_GrabTexture, grabTexcoord);

     		float4 nc = LinearBurn (c,_Color);
     		fixed dis = distance (float2(0.5,0.5),grabTexcoord) / distance(float2(0,0),float2(0.5,0.5)) * 2;
     		c = lerp(c,nc,dis);
            o.Albedo = c.rgb;
            o.Emission = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}
