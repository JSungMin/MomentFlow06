using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_UnityEngineAnimator : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		UnityEngine.Animator data = (UnityEngine.Animator)obj;
		// Add your writer.Write calls here.
writer.Write(data.rootPosition);
writer.Write(data.rootRotation);
writer.Write(data.applyRootMotion);
writer.Write(data.linearVelocityBlending);
writer.Write(data.updateMode);
writer.Write(data.bodyPosition);
writer.Write(data.bodyRotation);
writer.Write(data.stabilizeFeet);
writer.Write(data.feetPivotActive);
writer.Write(data.speed);
writer.Write(data.cullingMode);
writer.Write(data.playbackTime);
writer.Write(data.recorderStartTime);
writer.Write(data.recorderStopTime);
writer.Write(data.layersAffectMassCenter);
writer.Write(data.logWarnings);
writer.Write(data.fireEvents);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		UnityEngine.Animator data = GetOrCreate<UnityEngine.Animator>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		UnityEngine.Animator data = (UnityEngine.Animator)c;
		// Add your reader.Read calls here to read the data into the object.
data.rootPosition = reader.Read<UnityEngine.Vector3>();
data.rootRotation = reader.Read<UnityEngine.Quaternion>();
data.applyRootMotion = reader.Read<System.Boolean>();
data.linearVelocityBlending = reader.Read<System.Boolean>();
data.updateMode = reader.Read<UnityEngine.AnimatorUpdateMode>();
data.bodyPosition = reader.Read<UnityEngine.Vector3>();
data.bodyRotation = reader.Read<UnityEngine.Quaternion>();
data.stabilizeFeet = reader.Read<System.Boolean>();
data.feetPivotActive = reader.Read<System.Single>();
data.speed = reader.Read<System.Single>();
data.cullingMode = reader.Read<UnityEngine.AnimatorCullingMode>();
data.playbackTime = reader.Read<System.Single>();
data.recorderStartTime = reader.Read<System.Single>();
data.recorderStopTime = reader.Read<System.Single>();
data.layersAffectMassCenter = reader.Read<System.Boolean>();
data.logWarnings = reader.Read<System.Boolean>();
data.fireEvents = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_UnityEngineAnimator():base(typeof(UnityEngine.Animator)){}
}