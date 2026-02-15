using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AD RID: 173
	internal static class LODGroupRenderingUtils
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x00011D82 File Offset: 0x0000FF82
		public static float CalculateFOVHalfAngle(float fieldOfView)
		{
			return Mathf.Tan(0.017453292f * fieldOfView * 0.5f);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00011D98 File Offset: 0x0000FF98
		public static float CalculateScreenRelativeMetric(LODParameters lodParams, float lodBias)
		{
			float screenRelativeMetric;
			if (lodParams.isOrthographic)
			{
				screenRelativeMetric = 2f * lodParams.orthoSize;
			}
			else
			{
				float halfAngle = LODGroupRenderingUtils.CalculateFOVHalfAngle(lodParams.fieldOfView);
				screenRelativeMetric = 2f * halfAngle;
			}
			return screenRelativeMetric / lodBias;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		public static float CalculatePerspectiveDistance(Vector3 objPosition, Vector3 camPosition, float sqrScreenRelativeMetric)
		{
			return Mathf.Sqrt(LODGroupRenderingUtils.CalculateSqrPerspectiveDistance(objPosition, camPosition, sqrScreenRelativeMetric));
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		public static float CalculateSqrPerspectiveDistance(Vector3 objPosition, Vector3 camPosition, float sqrScreenRelativeMetric)
		{
			return (objPosition - camPosition).sqrMagnitude * sqrScreenRelativeMetric;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00011E06 File Offset: 0x00010006
		public static Vector3 GetWorldReferencePoint(this LODGroup lodGroup)
		{
			return lodGroup.transform.TransformPoint(lodGroup.localReferencePoint);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011E1C File Offset: 0x0001001C
		public static float GetWorldSpaceScale(this LODGroup lodGroup)
		{
			Vector3 scale = lodGroup.transform.lossyScale;
			return Mathf.Max(Mathf.Max(Mathf.Abs(scale.x), Mathf.Abs(scale.y)), Mathf.Abs(scale.z));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011E60 File Offset: 0x00010060
		public static float GetWorldSpaceSize(this LODGroup lodGroup)
		{
			return lodGroup.GetWorldSpaceScale() * lodGroup.size;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00011E6F File Offset: 0x0001006F
		public static float CalculateLODDistance(float relativeScreenHeight, float size)
		{
			return size / relativeScreenHeight;
		}
	}
}
