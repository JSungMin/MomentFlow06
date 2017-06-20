using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_SelfConditionChecker : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		SelfConditionChecker data = (SelfConditionChecker)obj;
		// Add your writer.Write calls here.
writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		SelfConditionChecker data = GetOrCreate<SelfConditionChecker>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		SelfConditionChecker data = (SelfConditionChecker)c;
		// Add your reader.Read calls here to read the data into the object.
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_SelfConditionChecker():base(typeof(SelfConditionChecker)){}
}