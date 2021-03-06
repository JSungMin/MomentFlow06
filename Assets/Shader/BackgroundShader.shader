﻿Shader "Custom/BackgroundShader"
{
	Properties
	{
		_OutsideColor ("OutsideColor", Color) = (0,0,0,0)
		_Radius ("Radius", Float) = 1
	}
	SubShader
	{
		Tags 
		{ 
			"RenderType"="Opaque" 
			"Queue" = "Transparent+1"
		}
		LOD 100

		GrabPass{}

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

			sampler2D _GrabTexture;

			fixed4 _OutsideColor;
			fixed _Radius;

			v2f vert (appdata v)
			{
				v2f o;
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);


				#if UNITY_UV_STARTS_AT_TOP
                 	float scale = -1.0;
            	#else
                 	float scale = 1.0;
             	#endif        
					o.uvgrab.xy = (float2(o.vertex.x, (o.vertex.y)* scale) + o.vertex.w) * 0.5;
                	o.uvgrab.zw = o.vertex.zw;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _OutsideColor;
				fixed4 grabColor = tex2D(_GrabTexture,i.uvgrab);

				col = SoftLight (col, grabColor);

				return col;
			}
			ENDCG
		}
	}
}
