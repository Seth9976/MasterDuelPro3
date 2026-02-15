using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000237 RID: 567
	internal static class RuntimeHelpersAbstraction
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
		public static bool IsWellKnownNoReferenceContainsType<T>()
		{
			return RuntimeHelpersAbstraction.WellKnownNoReferenceContainsType<T>.IsWellKnownType;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002D200 File Offset: 0x0002B400
		private static bool WellKnownNoReferenceContainsTypeInitialize(Type t)
		{
			if (t.IsPrimitive)
			{
				return true;
			}
			if (t.IsEnum)
			{
				return true;
			}
			if (t == typeof(DateTime))
			{
				return true;
			}
			if (t == typeof(DateTimeOffset))
			{
				return true;
			}
			if (t == typeof(Guid))
			{
				return true;
			}
			if (t == typeof(decimal))
			{
				return true;
			}
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return RuntimeHelpersAbstraction.WellKnownNoReferenceContainsTypeInitialize(t.GetGenericArguments()[0]);
			}
			return t == typeof(Vector2) || t == typeof(Vector3) || t == typeof(Vector4) || t == typeof(Color) || t == typeof(Rect) || t == typeof(Bounds) || t == typeof(Quaternion) || t == typeof(Vector2Int) || t == typeof(Vector3Int);
		}

		// Token: 0x02000238 RID: 568
		private static class WellKnownNoReferenceContainsType<T>
		{
			// Token: 0x04000683 RID: 1667
			public static readonly bool IsWellKnownType = RuntimeHelpersAbstraction.WellKnownNoReferenceContainsTypeInitialize(typeof(T));
		}
	}
}
