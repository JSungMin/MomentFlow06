using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_PlayerInfo : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		PlayerInfo data = (PlayerInfo)obj;
		// Add your writer.Write calls here.
writer.Write(data.perManaInc);writer.Write(data.hp);writer.Write(data.maxPocketCapacity);writer.Write(data.nowPocketCapacity);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		PlayerInfo data = GetOrCreate<PlayerInfo>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		PlayerInfo data = (PlayerInfo)c;
		// Add your reader.Read calls here to read the data into the object.
data.perManaInc = reader.Read<System.Single>();
data.hp = reader.Read<System.Single>();
data.maxPocketCapacity = reader.Read<System.Int32>();
data.nowPocketCapacity = reader.Read<System.Int32>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_PlayerInfo():base(typeof(PlayerInfo)){}
}