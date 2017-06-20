using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class ES2UserType_ComLuisPedroFonsecaProCamera2DProCamera2DTriggerZoom : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom data = (Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom)obj;
		// Add your writer.Write calls here.
writer.Write(data.SetSizeAsMultiplier);writer.Write(data.TargetZoom);writer.Write(data.ZoomSmoothness);writer.Write(data.ExclusiveInfluencePercentage);writer.Write(data.ResetSizeOnExit);writer.Write(data.ResetSizeSmoothness);writer.Write(data.UpdateInterval);writer.Write(data.TriggerShape);writer.Write(data.UseTargetsMidPoint);writer.Write(data.useGUILayout);
writer.Write(data.enabled);
writer.Write(data.hideFlags);

	}
	
	public override object Read(ES2Reader reader)
	{
		Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom data = GetOrCreate<Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom data = (Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom)c;
		// Add your reader.Read calls here to read the data into the object.
data.SetSizeAsMultiplier = reader.Read<System.Boolean>();
data.TargetZoom = reader.Read<System.Single>();
data.ZoomSmoothness = reader.Read<System.Single>();
data.ExclusiveInfluencePercentage = reader.Read<System.Single>();
data.ResetSizeOnExit = reader.Read<System.Boolean>();
data.ResetSizeSmoothness = reader.Read<System.Single>();
data.UpdateInterval = reader.Read<System.Single>();
data.TriggerShape = reader.Read<Com.LuisPedroFonseca.ProCamera2D.TriggerShape>();
data.UseTargetsMidPoint = reader.Read<System.Boolean>();
data.useGUILayout = reader.Read<System.Boolean>();
data.enabled = reader.Read<System.Boolean>();
data.hideFlags = reader.Read<UnityEngine.HideFlags>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_ComLuisPedroFonsecaProCamera2DProCamera2DTriggerZoom():base(typeof(Com.LuisPedroFonseca.ProCamera2D.ProCamera2DTriggerZoom)){}
}