using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001C4 RID: 452
	internal static class NativeArrayExtensions
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x000324D0 File Offset: 0x000306D0
		public static ref T UnsafeElementAt<T>(this NativeArray<T> array, int index) where T : struct
		{
			return UnsafeUtility.ArrayElementAsRef<T>(array.GetUnsafeReadOnlyPtr<T>(), index);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000324DE File Offset: 0x000306DE
		public static ref T UnsafeElementAtMutable<T>(this NativeArray<T> array, int index) where T : struct
		{
			return UnsafeUtility.ArrayElementAsRef<T>(array.GetUnsafePtr<T>(), index);
		}
	}
}
