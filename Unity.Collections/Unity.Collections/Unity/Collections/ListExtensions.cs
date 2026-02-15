using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000082 RID: 130
	public static class ListExtensions
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public static bool RemoveSwapBack<T>(this List<T> list, T value)
		{
			int index = list.IndexOf(value);
			if (index < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(index);
			return true;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00016BE4 File Offset: 0x00014DE4
		public static bool RemoveSwapBack<T>(this List<T> list, Predicate<T> matcher)
		{
			int index = list.FindIndex(matcher);
			if (index < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(index);
			return true;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00016C08 File Offset: 0x00014E08
		public static void RemoveAtSwapBack<T>(this List<T> list, int index)
		{
			int lastIndex = list.Count - 1;
			list[index] = list[lastIndex];
			list.RemoveAt(lastIndex);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00016C34 File Offset: 0x00014E34
		public static NativeList<T> ToNativeList<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this List<T> list, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeList<T> container = new NativeList<T>(list.Count, allocator);
			for (int i = 0; i < list.Count; i++)
			{
				container.AddNoResize(list[i]);
			}
			return container;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00016C70 File Offset: 0x00014E70
		public static NativeArray<T> ToNativeArray<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this List<T> list, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeArray<T> container = CollectionHelper.CreateNativeArray<T>(list.Count, allocator, NativeArrayOptions.UninitializedMemory);
			for (int i = 0; i < list.Count; i++)
			{
				container[i] = list[i];
			}
			return container;
		}
	}
}
