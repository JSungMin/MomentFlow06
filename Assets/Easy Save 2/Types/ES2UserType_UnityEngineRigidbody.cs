using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_UnityEngineRigidbody : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		UnityEngine.Rigidbody data = (UnityEngine.Rigidbody)obj;
		// Add your writer.Write calls here.
writer.Write(data.velocity);
writer.Write(data.angularVelocity);
writer.Write(data.drag);
writer.Write(data.angularDrag);
writer.Write(data.mass);
writer.Write(data.useGravity);
writer.Write(data.maxDepenetrationVelocity);
writer.Write(data.isKinematic);
writer.Write(data.freezeRotation);
writer.Write(data.constraints);
writer.Write(data.collisionDetectionMode);
writer.Write(data.centerOfMass);
writer.Write(data.inertiaTensorRotation);
writer.Write(data.inertiaTensor);
writer.Write(data.detectCollisions);
writer.Write(data.position);
writer.Write(data.rotation);
writer.Write(data.interpolation);
writer.Write(data.solverIterations);
writer.Write(data.solverVelocityIterations);
writer.Write(data.sleepThreshold);
writer.Write(data.maxAngularVelocity);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		UnityEngine.Rigidbody data = GetOrCreate<UnityEngine.Rigidbody>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		UnityEngine.Rigidbody data = (UnityEngine.Rigidbody)c;
		// Add your reader.Read calls here to read the data into the object.
data.velocity = reader.Read<UnityEngine.Vector3>();
data.angularVelocity = reader.Read<UnityEngine.Vector3>();
data.drag = reader.Read<System.Single>();
data.angularDrag = reader.Read<System.Single>();
data.mass = reader.Read<System.Single>();
data.useGravity = reader.Read<System.Boolean>();
data.maxDepenetrationVelocity = reader.Read<System.Single>();
data.isKinematic = reader.Read<System.Boolean>();
data.freezeRotation = reader.Read<System.Boolean>();
data.constraints = reader.Read<UnityEngine.RigidbodyConstraints>();
data.collisionDetectionMode = reader.Read<UnityEngine.CollisionDetectionMode>();
data.centerOfMass = reader.Read<UnityEngine.Vector3>();
data.inertiaTensorRotation = reader.Read<UnityEngine.Quaternion>();
data.inertiaTensor = reader.Read<UnityEngine.Vector3>();
data.detectCollisions = reader.Read<System.Boolean>();
data.position = reader.Read<UnityEngine.Vector3>();
data.rotation = reader.Read<UnityEngine.Quaternion>();
data.interpolation = reader.Read<UnityEngine.RigidbodyInterpolation>();
data.solverIterations = reader.Read<System.Int32>();
data.solverVelocityIterations = reader.Read<System.Int32>();
data.sleepThreshold = reader.Read<System.Single>();
data.maxAngularVelocity = reader.Read<System.Single>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_UnityEngineRigidbody():base(typeof(UnityEngine.Rigidbody)){}
}