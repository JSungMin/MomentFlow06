// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

#ifndef OUTLINE_INCLUDED
#define OUTLINE_INCLUDED

#include "UnityCG.cginc"

float3 _OutlineColor;
float _OutlineWidth, _OutlineFactor;

struct v2f
{
	float4 pos : SV_POSITION;
};
v2f vert (appdata_base v)
{
	float3 dir1 = normalize(v.vertex.xyz);
	float3 dir2 = v.normal;
	float3 dir = lerp(dir1, dir2, _OutlineFactor);
	dir = mul((float3x3)UNITY_MATRIX_IT_MV, dir);
	float2 offset = TransformViewToProjection(dir.xy);
	offset = normalize(offset);
	float dist = distance(mul(unity_ObjectToWorld, v.vertex), _WorldSpaceCameraPos);

	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.pos.xy += offset * o.pos.z * _OutlineWidth / dist;
	return o;
}
float4 frag (v2f i) : SV_TARGET
{
	return float4(_OutlineColor, 1);
}

#endif