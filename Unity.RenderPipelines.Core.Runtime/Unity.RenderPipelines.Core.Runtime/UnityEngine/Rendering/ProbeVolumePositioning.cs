using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000139 RID: 313
	internal static class ProbeVolumePositioning
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x00020070 File Offset: 0x0001E270
		public static bool OBBIntersect(in ProbeReferenceVolume.Volume a, in ProbeReferenceVolume.Volume b)
		{
			ProbeReferenceVolume.Volume volume = a;
			Vector3 aCenter;
			Vector3 aSize;
			volume.CalculateCenterAndSize(out aCenter, out aSize);
			volume = b;
			Vector3 bCenter;
			Vector3 bSize;
			volume.CalculateCenterAndSize(out bCenter, out bSize);
			float aRadius = aSize.sqrMagnitude / 2f;
			float bRadius = bSize.sqrMagnitude / 2f;
			if (Vector3.SqrMagnitude(aCenter - bCenter) > aRadius + bRadius)
			{
				return false;
			}
			Vector3[] axes = ProbeVolumePositioning.m_Axes;
			int num = 0;
			Vector3 vector = a.X;
			axes[num] = vector.normalized;
			Vector3[] axes2 = ProbeVolumePositioning.m_Axes;
			int num2 = 1;
			vector = a.Y;
			axes2[num2] = vector.normalized;
			Vector3[] axes3 = ProbeVolumePositioning.m_Axes;
			int num3 = 2;
			vector = a.Z;
			axes3[num3] = vector.normalized;
			Vector3[] axes4 = ProbeVolumePositioning.m_Axes;
			int num4 = 3;
			vector = b.X;
			axes4[num4] = vector.normalized;
			Vector3[] axes5 = ProbeVolumePositioning.m_Axes;
			int num5 = 4;
			vector = b.Y;
			axes5[num5] = vector.normalized;
			Vector3[] axes6 = ProbeVolumePositioning.m_Axes;
			int num6 = 5;
			vector = b.Z;
			axes6[num6] = vector.normalized;
			for (int i = 0; i < 6; i++)
			{
				Vector2 aProj = ProbeVolumePositioning.ProjectOBB(in a, ProbeVolumePositioning.m_Axes[i]);
				Vector2 bProj = ProbeVolumePositioning.ProjectOBB(in b, ProbeVolumePositioning.m_Axes[i]);
				if (aProj.y < bProj.x || bProj.y < aProj.x)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public static bool OBBContains(in ProbeReferenceVolume.Volume obb, Vector3 point)
		{
			Vector3 vector = obb.X;
			float lenX2 = vector.sqrMagnitude;
			vector = obb.Y;
			float lenY2 = vector.sqrMagnitude;
			vector = obb.Z;
			float lenZ2 = vector.sqrMagnitude;
			point -= obb.corner;
			point = new Vector3(Vector3.Dot(point, obb.X), Vector3.Dot(point, obb.Y), Vector3.Dot(point, obb.Z));
			return 0f < point.x && point.x < lenX2 && 0f < point.y && point.y < lenY2 && 0f < point.z && point.z < lenZ2;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002028C File Offset: 0x0001E48C
		public static bool OBBAABBIntersect(in ProbeReferenceVolume.Volume a, in Bounds b, in Bounds aAABB)
		{
			Bounds bounds = aAABB;
			if (!bounds.Intersects(b))
			{
				return false;
			}
			bounds = b;
			Vector3 boundsMin = bounds.min;
			bounds = b;
			Vector3 boundsMax = bounds.max;
			ProbeVolumePositioning.m_AABBCorners[0] = new Vector3(boundsMin.x, boundsMin.y, boundsMin.z);
			ProbeVolumePositioning.m_AABBCorners[1] = new Vector3(boundsMax.x, boundsMin.y, boundsMin.z);
			ProbeVolumePositioning.m_AABBCorners[2] = new Vector3(boundsMax.x, boundsMax.y, boundsMin.z);
			ProbeVolumePositioning.m_AABBCorners[3] = new Vector3(boundsMin.x, boundsMax.y, boundsMin.z);
			ProbeVolumePositioning.m_AABBCorners[4] = new Vector3(boundsMin.x, boundsMin.y, boundsMax.z);
			ProbeVolumePositioning.m_AABBCorners[5] = new Vector3(boundsMax.x, boundsMin.y, boundsMax.z);
			ProbeVolumePositioning.m_AABBCorners[6] = new Vector3(boundsMax.x, boundsMax.y, boundsMax.z);
			ProbeVolumePositioning.m_AABBCorners[7] = new Vector3(boundsMin.x, boundsMax.y, boundsMax.z);
			Vector3[] axes = ProbeVolumePositioning.m_Axes;
			int num = 0;
			Vector3 vector = a.X;
			axes[num] = vector.normalized;
			Vector3[] axes2 = ProbeVolumePositioning.m_Axes;
			int num2 = 1;
			vector = a.Y;
			axes2[num2] = vector.normalized;
			Vector3[] axes3 = ProbeVolumePositioning.m_Axes;
			int num3 = 2;
			vector = a.Z;
			axes3[num3] = vector.normalized;
			for (int i = 0; i < 3; i++)
			{
				Vector2 aProj = ProbeVolumePositioning.ProjectOBB(in a, ProbeVolumePositioning.m_Axes[i]);
				Vector2 bProj = ProbeVolumePositioning.ProjectAABB(in ProbeVolumePositioning.m_AABBCorners, ProbeVolumePositioning.m_Axes[i]);
				if (aProj.y < bProj.x || bProj.y < aProj.x)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002048C File Offset: 0x0001E68C
		private static Vector2 ProjectOBB(in ProbeReferenceVolume.Volume a, Vector3 axis)
		{
			float min = Vector3.Dot(axis, a.corner);
			float max = min;
			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < 2; y++)
				{
					for (int z = 0; z < 2; z++)
					{
						Vector3 vert = a.corner + a.X * (float)x + a.Y * (float)y + a.Z * (float)z;
						float proj = Vector3.Dot(axis, vert);
						if (proj < min)
						{
							min = proj;
						}
						else if (proj > max)
						{
							max = proj;
						}
					}
				}
			}
			return new Vector2(min, max);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00020534 File Offset: 0x0001E734
		private static Vector2 ProjectAABB(in Vector3[] corners, Vector3 axis)
		{
			float min = Vector3.Dot(axis, corners[0]);
			float max = min;
			for (int i = 1; i < 8; i++)
			{
				float proj = Vector3.Dot(axis, corners[i]);
				if (proj < min)
				{
					min = proj;
				}
				else if (proj > max)
				{
					max = proj;
				}
			}
			return new Vector2(min, max);
		}

		// Token: 0x040005D1 RID: 1489
		internal static Vector3[] m_Axes = new Vector3[6];

		// Token: 0x040005D2 RID: 1490
		internal static Vector3[] m_AABBCorners = new Vector3[8];
	}
}
