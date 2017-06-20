using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_AimTarget : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		AimTarget data = (AimTarget)obj;
		// Add your writer.Write calls here.
writer.Write(data.isActive);writer.Write(data.hideShoulder);writer.Write(data.maxAngle);writer.Write(data.minAngle);writer.Write(data.defaultAngle);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		AimTarget data = GetOrCreate<AimTarget>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		AimTarget data = (AimTarget)c;
		// Add your reader.Read calls here to read the data into the object.
data.isActive = reader.Read<System.Boolean>();
data.hideShoulder = reader.Read<System.Boolean>();
data.maxAngle = reader.Read<System.Single>();
data.minAngle = reader.Read<System.Single>();
data.defaultAngle = reader.Read<System.Single>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_AimTarget():base(typeof(AimTarget)){}
}