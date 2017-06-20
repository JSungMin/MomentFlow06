using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_Extinguisher : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Extinguisher data = (Extinguisher)obj;
		// Add your writer.Write calls here.
writer.Write(data.relativeExplosionPos);writer.Write(data.explosionRadius);writer.Write(data.explosionForce);writer.Write(data.isExplosing);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		Extinguisher data = GetOrCreate<Extinguisher>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Extinguisher data = (Extinguisher)c;
		// Add your reader.Read calls here to read the data into the object.
data.relativeExplosionPos = reader.Read<UnityEngine.Vector3>();
data.explosionRadius = reader.Read<System.Single>();
data.explosionForce = reader.Read<System.Single>();
data.isExplosing = reader.Read<System.Boolean>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_Extinguisher():base(typeof(Extinguisher)){}
}