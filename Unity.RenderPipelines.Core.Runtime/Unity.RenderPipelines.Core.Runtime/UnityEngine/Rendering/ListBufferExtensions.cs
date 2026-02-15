using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000051 RID: 81
	public static class ListBufferExtensions
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00008C2E File Offset: 0x00006E2E
		public unsafe static void QuickSort<[IsUnmanaged] T>(this ListBuffer<T> self) where T : struct, ValueType, IComparable<T>
		{
			CoreUnsafeUtils.QuickSort<int>(self.Count, (void*)self.BufferPtr);
		}
	}
}
