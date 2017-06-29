// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Ditortion"
{
    Properties
    {
        _MainTex ("Diffuse Texture", 2D) = "white" {}
    	_BumpMap ("Noise Text", 2D) = "bump" {}
    	_Magnitude ("Magnitude", Range (0, 1)) = 0.05
    }
     
    SubShader
    {
         Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
        }
        GrabPass {
        	"_GrabTexture"
        }
        Pass
        {
        	
            AlphaTest Greater 0.0     // Pixel with an alpha of 0 should be ignored
             
            CGPROGRAM
              
            #pragma vertex vert
            #pragma fragment frag
              
            #include "UnityCG.cginc"
              
            // User-specified properties
            uniform sampler2D _MainTex;
            uniform sampler2D _GrabTexture;
          	uniform sampler2D _BumpMap;
			float  _Magnitude;

            struct VertexInput
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
                float4 color : COLOR;
            };
              
            struct VertexOutput
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float intensity : TEXCOORD1;
                float4 uvgrab : TEXCOORD2;
            };
              
            VertexOutput vert(VertexInput input)
            {
                VertexOutput output;
                output.pos = UnityObjectToClipPos(input.uv);
                output.uvgrab = ComputeGrabScreenPos(output.pos);
                output.uv = input.uv;
                output.color = input.color;
         
                return output;
            }
              
            float4 frag(VertexOutput input) : COLOR
            {

           	 	half4 bump = tex2D (_BumpMap, input.uv);
           	 	half2 distortion =	UnpackNormal (bump).rg;

           	 	input.uvgrab.xy += distortion * _Magnitude;

           	 	float4 grabColor = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(input.uvgrab));

                float4 diffuseColor = tex2D(_MainTex, input.uv);

               	grabColor.rgb * 0.5;
     
                return grabColor;
              }
          
               ENDCG
           }
    }
}