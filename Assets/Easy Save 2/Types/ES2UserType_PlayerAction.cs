using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_PlayerAction : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		PlayerAction data = (PlayerAction)obj;
		// Add your writer.Write calls here.
writer.Write(data.holdOnWeapon);writer.Write(data.velocity);writer.Write(data.distanceToGround);writer.Write(data.maxSpeed);writer.Write(data.accel);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		PlayerAction data = GetOrCreate<PlayerAction>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		PlayerAction data = (PlayerAction)c;
		// Add your reader.Read calls here to read the data into the object.
data.holdOnWeapon = reader.Read<System.Boolean>();
data.velocity = reader.Read<UnityEngine.Vector3>();
data.distanceToGround = reader.Read<System.Single>();
data.maxSpeed = reader.Read<System.Single>();
data.accel = reader.Read<System.Single>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_PlayerAction():base(typeof(PlayerAction)){}
}