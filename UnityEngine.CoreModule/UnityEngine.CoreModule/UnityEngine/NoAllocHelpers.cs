using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	// Token: 0x020001AC RID: 428
	internal static class NoAllocHelpers
	{
		// Token: 0x060010FF RID: 4351 RVA: 0x0002425C File Offset: 0x0002245C
		public unsafe static void EnsureListElemCount<T>(List<T> list, int count)
		{
			bool flag = list == null;
			if (flag)
			{
				throw new ArgumentNullException("list");
			}
			bool flag2 = count < 0;
			if (flag2)
			{
				throw new ArgumentException("invalid size to resize.", "list");
			}
			list.Clear();
			bool flag3 = list.Capacity < count;
			if (flag3)
			{
				list.Capacity = count;
			}
			bool flag4 = count != list.Count;
			if (flag4)
			{
				NoAllocHelpers.ListPrivateFieldAccess<T> tListAccess = *UnsafeUtility.As<List<T>, NoAllocHelpers.ListPrivateFieldAccess<T>>(ref list);
				tListAccess._size = count;
				tListAccess._version++;
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000242E4 File Offset: 0x000224E4
		public static int SafeLength(Array values)
		{
			return (values != null) ? values.Length : 0;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00024304 File Offset: 0x00022504
		public static int SafeLength<T>(List<T> values)
		{
			return (values != null) ? values.Count : 0;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00024324 File Offset: 0x00022524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T[] ExtractArrayFromList<T>(List<T> list)
		{
			bool flag = list == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				NoAllocHelpers.ListPrivateFieldAccess<T> tListAccess = UnsafeUtility.As<NoAllocHelpers.ListPrivateFieldAccess<T>>(list);
				array = tListAccess._items;
			}
			return array;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00024350 File Offset: 0x00022550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetListContents<T>(List<T> list, ReadOnlySpan<T> span)
		{
			NoAllocHelpers.ListPrivateFieldAccess<T> tListAccess = UnsafeUtility.As<NoAllocHelpers.ListPrivateFieldAccess<T>>(list);
			tListAccess._items = span.ToArray();
			tListAccess._size = span.Length;
			tListAccess._version++;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00024390 File Offset: 0x00022590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ResetListSize<[IsUnmanaged] T>(List<T> list, int size) where T : struct, ValueType
		{
			Debug.Assert(list.Capacity >= size);
			NoAllocHelpers.ListPrivateFieldAccess<T> tListAccess = UnsafeUtility.As<NoAllocHelpers.ListPrivateFieldAccess<T>>(list);
			tListAccess._size = size;
			tListAccess._version++;
		}

		// Token: 0x020001AD RID: 429
		private class ListPrivateFieldAccess<T>
		{
			// Token: 0x04000676 RID: 1654
			internal T[] _items;

			// Token: 0x04000677 RID: 1655
			internal int _size;

			// Token: 0x04000678 RID: 1656
			internal int _version;
		}
	}
}
