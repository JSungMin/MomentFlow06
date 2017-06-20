using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_UnityEngineParticleSystem : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		UnityEngine.ParticleSystem data = (UnityEngine.ParticleSystem)obj;
		// Add your writer.Write calls here.
writer.Write(data.time);
writer.Write(data.randomSeed);
writer.Write(data.useAutoRandomSeed);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		UnityEngine.ParticleSystem data = GetOrCreate<UnityEngine.ParticleSystem>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		UnityEngine.ParticleSystem data = (UnityEngine.ParticleSystem)c;
		// Add your reader.Read calls here to read the data into the object.
data.time = reader.Read<System.Single>();
data.randomSeed = reader.Read<System.UInt32>();
data.useAutoRandomSeed = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_UnityEngineParticleSystem():base(typeof(UnityEngine.ParticleSystem)){}
}