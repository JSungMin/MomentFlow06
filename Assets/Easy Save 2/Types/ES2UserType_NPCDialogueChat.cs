using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_NPCDialogueChat : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		NPCDialogueChat data = (NPCDialogueChat)obj;
		// Add your writer.Write calls here.
writer.Write(data.showDelay);writer.Write(data.pageNumber);writer.Write(data.flipTimer);writer.Write(data.tryCountForActivate);writer.Write(data.maxActionCircle);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		NPCDialogueChat data = GetOrCreate<NPCDialogueChat>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		NPCDialogueChat data = (NPCDialogueChat)c;
		// Add your reader.Read calls here to read the data into the object.
data.showDelay = reader.Read<System.Single>();
data.pageNumber = reader.Read<System.Int32>();
data.flipTimer = reader.Read<System.Single>();
data.tryCountForActivate = reader.Read<System.Int32>();
data.maxActionCircle = reader.Read<System.Int32>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_NPCDialogueChat():base(typeof(NPCDialogueChat)){}
}