using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_EnemyInfo : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		EnemyInfo data = (EnemyInfo)obj;
		// Add your writer.Write calls here.
writer.Write(data.teamId);writer.Write(data.viewHeightScale);writer.Write(data.isUpdatable);writer.Write(data.hp);writer.Write(data.maxPocketCapacity);writer.Write(data.nowPocketCapacity);writer.Write(data.AimPos);
writer.Write(data.Hp);
writer.Write(data.AttackDelayTimer);
writer.Write(data.CrouchDelayTimer);
writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		EnemyInfo data = GetOrCreate<EnemyInfo>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		EnemyInfo data = (EnemyInfo)c;
		// Add your reader.Read calls here to read the data into the object.
data.teamId = reader.Read<System.Int32>();
data.viewHeightScale = reader.Read<System.Single>();
data.isUpdatable = reader.Read<System.Boolean>();
data.hp = reader.Read<System.Single>();
data.maxPocketCapacity = reader.Read<System.Int32>();
data.nowPocketCapacity = reader.Read<System.Int32>();
data.AimPos = reader.Read<UnityEngine.Vector3>();
data.Hp = reader.Read<System.Single>();
data.AttackDelayTimer = reader.Read<System.Single>();
data.CrouchDelayTimer = reader.Read<System.Single>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_EnemyInfo():base(typeof(EnemyInfo)){}
}