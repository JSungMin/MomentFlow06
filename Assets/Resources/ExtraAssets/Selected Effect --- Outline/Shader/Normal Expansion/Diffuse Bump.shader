﻿Shader "Selected Effect --- Outline/Normal Expansion/Diffuse Bump" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
		_BumpMap ("Bump", 2D) = "bump" {}
		_OutlineWidth ("Outline Width", Float) = 0.1
		_OutlineColor ("Outline Color", Color) = (0, 1, 0, 1)
		_OutlineFactor ("Outline Factor", Range(0, 1)) = 1
		_Overlay ("Overlay", Float) = 0
		_OverlayColor ("Overlay Color", Color) = (1, 1, 0, 1)
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		Pass {
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Outline.cginc"
			ENDCG
		}

		CGPROGRAM
		#pragma surface surf Lambert
		sampler2D _MainTex, _BumpMap;
		float4 _OverlayColor;
		float _Overlay;
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = lerp(c.rgb, _OverlayColor.rgb, _Overlay);
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
