using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_CutSceneUnit : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		CutSceneUnit data = (CutSceneUnit)obj;
		// Add your writer.Write calls here.
writer.Write(data.pinPath);writer.Write(data.nowTrackIndex);writer.Write(data.isAction);writer.Write(data.wrapMode);writer.Write(data.moveMethod);writer.Write(data.color);writer.Write(data.timer);writer.Write(data.isPong);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		CutSceneUnit data = GetOrCreate<CutSceneUnit>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		CutSceneUnit data = (CutSceneUnit)c;
		// Add your reader.Read calls here to read the data into the object.
data.pinPath = reader.Read<System.Boolean>();
data.nowTrackIndex = reader.Read<System.Int32>();
data.isAction = reader.Read<System.Boolean>();
data.wrapMode = reader.Read<Struct.CutSceneWrapMode>();
data.moveMethod = reader.Read<Struct.MoveMethod>();
data.color = reader.Read<UnityEngine.Color>();
data.timer = reader.Read<System.Single>();
data.isPong = reader.Read<System.Boolean>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_CutSceneUnit():base(typeof(CutSceneUnit)){}
}