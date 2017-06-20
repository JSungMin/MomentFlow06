using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_UnityEngineSpriteRenderer : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		UnityEngine.SpriteRenderer data = (UnityEngine.SpriteRenderer)obj;
		// Add your writer.Write calls here.
writer.Write(data.drawMode);
writer.Write(data.size);
writer.Write(data.adaptiveModeThreshold);
writer.Write(data.tileMode);
writer.Write(data.color);
writer.Write(data.flipX);
writer.Write(data.flipY);
writer.Write(data.enabled);
writer.Write(data.shadowCastingMode);
writer.Write(data.receiveShadows);
writer.Write(data.lightmapIndex);
writer.Write(data.realtimeLightmapIndex);
writer.Write(data.lightmapScaleOffset);
writer.Write(data.motionVectorGenerationMode);
writer.Write(data.realtimeLightmapScaleOffset);
writer.Write(data.lightProbeUsage);
writer.Write(data.reflectionProbeUsage);
writer.Write(data.sortingLayerID);
writer.Write(data.sortingOrder);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		UnityEngine.SpriteRenderer data = GetOrCreate<UnityEngine.SpriteRenderer>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		UnityEngine.SpriteRenderer data = (UnityEngine.SpriteRenderer)c;
		// Add your reader.Read calls here to read the data into the object.
data.drawMode = reader.Read<UnityEngine.SpriteDrawMode>();
data.size = reader.Read<UnityEngine.Vector2>();
data.adaptiveModeThreshold = reader.Read<System.Single>();
data.tileMode = reader.Read<UnityEngine.SpriteTileMode>();
data.color = reader.Read<UnityEngine.Color>();
data.flipX = reader.Read<System.Boolean>();
data.flipY = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.shadowCastingMode = reader.Read<UnityEngine.Rendering.ShadowCastingMode>();
data.receiveShadows = reader.Read<System.Boolean>();
data.lightmapIndex = reader.Read<System.Int32>();
data.realtimeLightmapIndex = reader.Read<System.Int32>();
data.lightmapScaleOffset = reader.Read<UnityEngine.Vector4>();
data.motionVectorGenerationMode = reader.Read<UnityEngine.MotionVectorGenerationMode>();
data.realtimeLightmapScaleOffset = reader.Read<UnityEngine.Vector4>();
data.lightProbeUsage = reader.Read<UnityEngine.Rendering.LightProbeUsage>();
data.reflectionProbeUsage = reader.Read<UnityEngine.Rendering.ReflectionProbeUsage>();
data.sortingLayerID = reader.Read<System.Int32>();
data.sortingOrder = reader.Read<System.Int32>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_UnityEngineSpriteRenderer():base(typeof(UnityEngine.SpriteRenderer)){}
}