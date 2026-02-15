using System;
using System.Reflection;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x020000B7 RID: 183
	public static class DOTweenUtils
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x000131E8 File Offset: 0x000113E8
		internal static Vector3 Vector3FromAngle(float degrees, float magnitude)
		{
			float num = degrees * 0.017453292f;
			return new Vector3(magnitude * Mathf.Cos(num), magnitude * Mathf.Sin(num), 0f);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00013218 File Offset: 0x00011418
		internal static float Angle2D(Vector3 from, Vector3 to)
		{
			Vector2 right = Vector2.right;
			to -= from;
			float num = Vector2.Angle(right, to);
			if (Vector3.Cross(right, to).z > 0f)
			{
				num = 360f - num;
			}
			return num * -1f;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00013268 File Offset: 0x00011468
		internal static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
		{
			return rotation * (point - pivot) + pivot;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00013280 File Offset: 0x00011480
		public static Vector2 GetPointOnCircle(Vector2 center, float radius, float degrees)
		{
			degrees = 90f - degrees;
			float num = degrees * 0.017453292f;
			return center + new Vector2(Mathf.Cos(num), Mathf.Sin(num)) * radius;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000132BB File Offset: 0x000114BB
		internal static bool Vector3AreApproximatelyEqual(Vector3 a, Vector3 b)
		{
			return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000132F8 File Offset: 0x000114F8
		internal static Type GetLooseScriptType(string typeName)
		{
			for (int i = 0; i < DOTweenUtils._defAssembliesToQuery.Length; i++)
			{
				Type type = Type.GetType(string.Format("{0}, {1}", typeName, DOTweenUtils._defAssembliesToQuery[i]));
				if (type != null)
				{
					return type;
				}
			}
			if (DOTweenUtils._loadedAssemblies == null)
			{
				DOTweenUtils._loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			}
			for (int j = 0; j < DOTweenUtils._loadedAssemblies.Length; j++)
			{
				Type type2 = Type.GetType(string.Format("{0}, {1}", typeName, DOTweenUtils._loadedAssemblies[j].GetName()));
				if (type2 != null)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x0400024A RID: 586
		private static Assembly[] _loadedAssemblies;

		// Token: 0x0400024B RID: 587
		private static readonly string[] _defAssembliesToQuery = new string[] { "DOTween.Modules", "Assembly-CSharp", "Assembly-CSharp-firstpass" };
	}
}
