using System.Collections.Generic;
using UnityEngine;

public sealed class ES2_GameObject : ES2Type
{
	public ES2_GameObject() : base(typeof(GameObject)) { }

	public override void Write(object data, ES2Writer writer)
	{
		GameObject go = (GameObject)data;
		// Get the Components of the GameObject that you want to save and save them.
		var components = go.GetComponents(typeof(Component));
		var supportedComponents = new List<Component>();

		// Get the supported Components and put them in a List.
		foreach (var component in components)
			if (ES2TypeManager.GetES2Type(component.GetType()) != null)
				supportedComponents.Add(component);

		// Write how many Components we're saving so we know when we're loading.
		writer.Write(supportedComponents.Count);

		// Save each Component individually.
		foreach (var component in supportedComponents)
		{
			var es2Type = ES2TypeManager.GetES2Type(component.GetType());
			writer.Write(es2Type.hash);
			es2Type.Write(component, writer);
		}
	}

	public override void Read(ES2Reader reader, object obj)
	{
		GameObject go = (GameObject)obj;
		// How many components do we need to load?
		int componentCount = reader.Read<int>();

		for (int i = 0; i < componentCount; i++)
		{
			// Get the ES2Type using the hash stored before the component.
			var es2Type = ES2TypeManager.GetES2Type(reader.Read<int>());
			// Get Component from GameObject, or add it if it doesn't have one.
			Component c = go.GetComponent (es2Type.type);
			if(c == null)
				c = go.AddComponent(es2Type.type);
			// Use the ES2Type to read.
			es2Type.Read(reader, c);
		}
	}

	public override object Read(ES2Reader reader)
	{
		GameObject go = new GameObject();
		Read(reader, go);
		return go;
	}
}