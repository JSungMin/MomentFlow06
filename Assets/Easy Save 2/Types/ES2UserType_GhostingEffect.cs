using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_GhostingEffect : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		GhostingEffect data = (GhostingEffect)obj;
		// Add your writer.Write calls here.
writer.Write(data.segments);writer.Write(data.trailTime);writer.Write(data.spawnInterval);writer.Write(data.spawnTimer);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		GhostingEffect data = GetOrCreate<GhostingEffect>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		GhostingEffect data = (GhostingEffect)c;
		// Add your reader.Read calls here to read the data into the object.
data.segments = reader.Read<System.Int32>();
data.trailTime = reader.Read<System.Single>();
data.spawnInterval = reader.Read<System.Single>();
data.spawnTimer = reader.Read<System.Single>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_GhostingEffect():base(typeof(GhostingEffect)){}
}