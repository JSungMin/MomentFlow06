Shader "Custom/DamageEffectShader"
{
	Properties
	{
		_MainTex ("Main Tex(RGB)", 2D) = "white" {}
		_InsideColor ("AmbientColor", Color) = (0,0,0,0)
	}
	SubShader
	{
		Tags 
		{ 
			"RenderType"="Opaque" 
			"Queue" = "Transparent+1"
		}
		LOD 100

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			//BlendOp Max

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			#include "Assets/Shader/BlendMode.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 uvgrab : TEXCOORD1;
			};

			sampler2D _MainTex;
			fixed4 _InsideColor;

			v2f vert (appdata v)
			{
				v2f o;
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D (_MainTex, i.uv);
			
				col.rgb = _InsideColor.rgb;
				if (col.a < 0.5)
					col.a = 0;
				col.rgb *= col.a;

				return col;
			}
			ENDCG
		}
	}
}
