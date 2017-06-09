// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Selected Effect --- Outline/Sprite" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
		_Color ("Tint", Color) = (1, 1, 1, 1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_OutlineColor ("Outline Color", Color) = (1, 1, 1, 1)
		_OutlineSize ("Outline Size", int) = 1
	}
	SubShader {
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _Color, _OutlineColor, _MainTex_TexelSize;
			int _OutlineSize;
			
			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			v2f vert (appdata_full v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color * _Color;
#ifdef PIXELSNAP_ON
				o.vertex = UnityPixelSnap(o.vertex);
#endif
				return o;
			}
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.texcoord) * i.color;
				if (c.a != 0) {
					float totalAlpha = 1.0;

					[unroll(16)]
					for (int n = 1; n < _OutlineSize + 1; n++) {
						fixed4 up = tex2D(_MainTex,    i.texcoord + fixed2(0, n * _MainTex_TexelSize.y));
						fixed4 down = tex2D(_MainTex,  i.texcoord - fixed2(0, n *  _MainTex_TexelSize.y));
						fixed4 right = tex2D(_MainTex, i.texcoord + fixed2(n * _MainTex_TexelSize.x, 0));
						fixed4 left = tex2D(_MainTex,  i.texcoord - fixed2(n * _MainTex_TexelSize.x, 0));
						totalAlpha = totalAlpha * up.a * down.a * right.a * left.a;
					}

					if (totalAlpha == 0)
						c.rgba = _OutlineColor;
				}
				c.rgb *= c.a;
				return c;
			}
			ENDCG
		}
	}
}