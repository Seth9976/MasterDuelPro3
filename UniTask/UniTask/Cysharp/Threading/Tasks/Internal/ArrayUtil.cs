using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200022D RID: 557
	internal static class ArrayUtil
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x0002BF53 File Offset: 0x0002A153
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void EnsureCapacity<T>(ref T[] array, int index)
		{
			if (array.Length <= index)
			{
				ArrayUtil.EnsureCore<T>(ref array, index);
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002BF64 File Offset: 0x0002A164
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void EnsureCore<T>(ref T[] array, int index)
		{
			int newSize = array.Length * 2;
			T[] newArray = new T[(index < newSize) ? newSize : (index * 2)];
			Array.Copy(array, 0, newArray, 0, array.Length);
			array = newArray;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002BF9C File Offset: 0x0002A19C
		[return: TupleElementNames(new string[] { "array", "length" })]
		public static ValueTuple<T[], int> Materialize<T>(IEnumerable<T> source)
		{
			T[] array = source as T[];
			if (array != null)
			{
				return new ValueTuple<T[], int>(array, array.Length);
			}
			int defaultCount = 4;
			ICollection<T> coll = source as ICollection<T>;
			if (coll != null)
			{
				defaultCount = coll.Count;
				T[] buffer = new T[defaultCount];
				coll.CopyTo(buffer, 0);
				return new ValueTuple<T[], int>(buffer, defaultCount);
			}
			IReadOnlyCollection<T> rcoll = source as IReadOnlyCollection<T>;
			if (rcoll != null)
			{
				defaultCount = rcoll.Count;
			}
			if (defaultCount == 0)
			{
				return new ValueTuple<T[], int>(Array.Empty<T>(), 0);
			}
			int index = 0;
			T[] buffer2 = new T[defaultCount];
			foreach (T item in source)
			{
				ArrayUtil.EnsureCapacity<T>(ref buffer2, index);
				buffer2[index++] = item;
			}
			return new ValueTuple<T[], int>(buffer2, index);
		}
	}
}
