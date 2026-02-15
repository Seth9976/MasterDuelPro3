using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
internal static class ShadowShapeProvider2DUtility
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static float GetTrimEdgeFromBounds(Bounds bounds, float trimMultipler)
	{
		Vector3 size = bounds.size;
		float trimEdge = trimMultipler * ((size.x < size.y) ? size.x : size.y);
		float multiplier = Mathf.Pow(10f, -Mathf.Floor(Mathf.Log10(trimEdge)));
		return Mathf.Floor(trimEdge * multiplier) / multiplier;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x000020A7 File Offset: 0x000002A7
	public static bool IsUsingGpuDeformation()
	{
		return false;
	}
}
