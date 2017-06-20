using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_EquiptInfo : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		EquiptInfo data = (EquiptInfo)obj;
		// Add your writer.Write calls here.
writer.Write(data.nowEquiptWeaponIndex);writer.Write(data.defaultEquiptWeaponId);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		EquiptInfo data = GetOrCreate<EquiptInfo>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		EquiptInfo data = (EquiptInfo)c;
		// Add your reader.Read calls here to read the data into the object.
data.nowEquiptWeaponIndex = reader.Read<System.Int32>();
data.defaultEquiptWeaponId = reader.Read<System.Int32>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_EquiptInfo():base(typeof(EquiptInfo)){}
}