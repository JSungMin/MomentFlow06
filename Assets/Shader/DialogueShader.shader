Shader "Custom/ScatterSprite"
{
	Properties
	{
		_MainTex ("Main Tex(RGB)", 2D) = "white" {}
		_InsideColor ("AmbientColor", Color) = (0,0,0,0)
		_Tiling ("Tile Amount", Vector) = (1,1,0,0)
		_Timer ("Timer", Float) = 1
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
			fixed4 _Tiling;
			fixed _Timer;

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
				fixed4 col = tex2D (_MainTex, i.uv + float2 (0.2,0.2));

				fixed2 area = fixed2(1/_Tiling.x,1/_Tiling.y);
				fixed2 value = fixed2(i.uv.x / area.x, i.uv.y / area.y);
				fixed2 remaining = fixed2(i.uv.x % area.x, i.uv.y % area.y);

				//for (int i = 0; i < _Tiling.x; i++)
				//{
				//	for (int j = 0; j < _Tiling.y; j++)
				//	{
				//		col += tex2D (_MainTex, );
				//	}
				//}

				fixed4 display = col + tex2D (_MainTex, i.uv + float2(0.5,0.5));
				_Timer -= _Time.y;
				display.rgb = col.rgb;
				display.rgb *= display.a;

				return display;
			}
			ENDCG
		}
	}
}
