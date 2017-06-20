using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_Door : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Door data = (Door)obj;
		// Add your writer.Write calls here.
writer.Write(data.isLocked);writer.Write(data.isInteracted);writer.Write(data.isConnected);writer.Write(data.objectType);writer.Write(data.IsLocked);
writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		Door data = GetOrCreate<Door>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Door data = (Door)c;
		// Add your reader.Read calls here to read the data into the object.
data.isLocked = reader.Read<System.Boolean>();
data.isInteracted = reader.Read<System.Boolean>();
data.isConnected = reader.Read<System.Boolean>();
data.objectType = reader.Read<InteractableObjectType>();
data.IsLocked = reader.Read<System.Boolean>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_Door():base(typeof(Door)){}
}