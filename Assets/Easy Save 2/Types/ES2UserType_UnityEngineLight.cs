using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_UnityEngineLight : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		UnityEngine.Light data = (UnityEngine.Light)obj;
		// Add your writer.Write calls here.
writer.Write(data.type);
writer.Write(data.color);
writer.Write(data.colorTemperature);
writer.Write(data.intensity);
writer.Write(data.bounceIntensity);
writer.Write(data.shadows);
writer.Write(data.shadowStrength);
writer.Write(data.shadowResolution);
writer.Write(data.shadowCustomResolution);
writer.Write(data.shadowBias);
writer.Write(data.shadowNormalBias);
writer.Write(data.shadowNearPlane);
writer.Write(data.range);
writer.Write(data.spotAngle);
writer.Write(data.cookieSize);
writer.Write(data.renderMode);
writer.Write(data.alreadyLightmapped);
writer.Write(data.cullingMask);
writer.Write(data.areaSize);
writer.Write(data.lightmapBakeType);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		UnityEngine.Light data = GetOrCreate<UnityEngine.Light>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		UnityEngine.Light data = (UnityEngine.Light)c;
		// Add your reader.Read calls here to read the data into the object.
data.type = reader.Read<UnityEngine.LightType>();
data.color = reader.Read<UnityEngine.Color>();
data.colorTemperature = reader.Read<System.Single>();
data.intensity = reader.Read<System.Single>();
data.bounceIntensity = reader.Read<System.Single>();
data.shadows = reader.Read<UnityEngine.LightShadows>();
data.shadowStrength = reader.Read<System.Single>();
data.shadowResolution = reader.Read<UnityEngine.Rendering.LightShadowResolution>();
data.shadowCustomResolution = reader.Read<System.Int32>();
data.shadowBias = reader.Read<System.Single>();
data.shadowNormalBias = reader.Read<System.Single>();
data.shadowNearPlane = reader.Read<System.Single>();
data.range = reader.Read<System.Single>();
data.spotAngle = reader.Read<System.Single>();
data.cookieSize = reader.Read<System.Single>();
data.renderMode = reader.Read<UnityEngine.LightRenderMode>();
data.alreadyLightmapped = reader.Read<System.Boolean>();
data.cullingMask = reader.Read<System.Int32>();
data.areaSize = reader.Read<UnityEngine.Vector2>();
data.lightmapBakeType = reader.Read<UnityEngine.LightmapBakeType>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_UnityEngineLight():base(typeof(UnityEngine.Light)){}
}