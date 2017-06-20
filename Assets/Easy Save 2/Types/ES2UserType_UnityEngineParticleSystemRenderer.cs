using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_UnityEngineParticleSystemRenderer : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		UnityEngine.ParticleSystemRenderer data = (UnityEngine.ParticleSystemRenderer)obj;
		// Add your writer.Write calls here.
writer.Write(data.renderMode);
writer.Write(data.lengthScale);
writer.Write(data.velocityScale);
writer.Write(data.cameraVelocityScale);
writer.Write(data.normalDirection);
writer.Write(data.alignment);
writer.Write(data.pivot);
writer.Write(data.sortMode);
writer.Write(data.sortingFudge);
writer.Write(data.minParticleSize);
writer.Write(data.maxParticleSize);
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
		UnityEngine.ParticleSystemRenderer data = GetOrCreate<UnityEngine.ParticleSystemRenderer>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		UnityEngine.ParticleSystemRenderer data = (UnityEngine.ParticleSystemRenderer)c;
		// Add your reader.Read calls here to read the data into the object.
data.renderMode = reader.Read<UnityEngine.ParticleSystemRenderMode>();
data.lengthScale = reader.Read<System.Single>();
data.velocityScale = reader.Read<System.Single>();
data.cameraVelocityScale = reader.Read<System.Single>();
data.normalDirection = reader.Read<System.Single>();
data.alignment = reader.Read<UnityEngine.ParticleSystemRenderSpace>();
data.pivot = reader.Read<UnityEngine.Vector3>();
data.sortMode = reader.Read<UnityEngine.ParticleSystemSortMode>();
data.sortingFudge = reader.Read<System.Single>();
data.minParticleSize = reader.Read<System.Single>();
data.maxParticleSize = reader.Read<System.Single>();
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
	public ES2UserType_UnityEngineParticleSystemRenderer():base(typeof(UnityEngine.ParticleSystemRenderer)){}
}