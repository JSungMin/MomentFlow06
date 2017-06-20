using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_Weapon : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Weapon data = (Weapon)obj;
		// Add your writer.Write calls here.
writer.Write(data.id);writer.Write(data.weaponType);writer.Write(data.damage);writer.Write(data.attackDelay);writer.Write(data.attackDelayTimer);
	}
	
	public override object Read(ES2Reader reader)
	{
		Weapon data = new Weapon();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Weapon data = (Weapon)c;
		// Add your reader.Read calls here to read the data into the object.
data.id = reader.Read<System.Int32>();
data.weaponType = reader.Read<WeaponType>();
data.damage = reader.Read<System.Int32>();
data.attackDelay = reader.Read<System.Single>();
data.attackDelayTimer = reader.Read<System.Single>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_Weapon():base(typeof(Weapon)){}
}