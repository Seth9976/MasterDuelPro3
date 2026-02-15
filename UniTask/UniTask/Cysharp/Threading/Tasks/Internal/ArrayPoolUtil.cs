using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200022B RID: 555
	internal static class ArrayPoolUtil
	{
		// Token: 0x06000C94 RID: 3220 RVA: 0x0002BD87 File Offset: 0x00029F87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void EnsureCapacity<T>(ref T[] array, int index, ArrayPool<T> pool)
		{
			if (array.Length <= index)
			{
				ArrayPoolUtil.EnsureCapacityCore<T>(ref array, index, pool);
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002BD98 File Offset: 0x00029F98
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void EnsureCapacityCore<T>(ref T[] array, int index, ArrayPool<T> pool)
		{
			if (array.Length <= index)
			{
				int newSize = array.Length * 2;
				T[] newArray = pool.Rent((index < newSize) ? newSize : (index * 2));
				Array.Copy(array, 0, newArray, 0, array.Length);
				pool.Return(array, !RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<T>());
				array = newArray;
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002BDE8 File Offset: 0x00029FE8
		public static ArrayPoolUtil.RentArray<T> Materialize<T>(IEnumerable<T> source)
		{
			T[] array = source as T[];
			if (array != null)
			{
				return new ArrayPoolUtil.RentArray<T>(array, array.Length, null);
			}
			int defaultCount = 32;
			ICollection<T> coll = source as ICollection<T>;
			if (coll != null)
			{
				if (coll.Count == 0)
				{
					return new ArrayPoolUtil.RentArray<T>(Array.Empty<T>(), 0, null);
				}
				defaultCount = coll.Count;
				ArrayPool<T> pool = ArrayPool<T>.Shared;
				T[] buffer = pool.Rent(defaultCount);
				coll.CopyTo(buffer, 0);
				return new ArrayPoolUtil.RentArray<T>(buffer, coll.Count, pool);
			}
			else
			{
				IReadOnlyCollection<T> rcoll = source as IReadOnlyCollection<T>;
				if (rcoll != null)
				{
					defaultCount = rcoll.Count;
				}
				if (defaultCount == 0)
				{
					return new ArrayPoolUtil.RentArray<T>(Array.Empty<T>(), 0, null);
				}
				ArrayPool<T> pool2 = ArrayPool<T>.Shared;
				int index = 0;
				T[] buffer2 = pool2.Rent(defaultCount);
				foreach (T item in source)
				{
					ArrayPoolUtil.EnsureCapacity<T>(ref buffer2, index, pool2);
					buffer2[index++] = item;
				}
				return new ArrayPoolUtil.RentArray<T>(buffer2, index, pool2);
			}
		}

		// Token: 0x0200022C RID: 556
		public struct RentArray<T> : IDisposable
		{
			// Token: 0x06000C97 RID: 3223 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
			public RentArray(T[] array, int length, ArrayPool<T> pool)
			{
				this.Array = array;
				this.Length = length;
				this.pool = pool;
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x0002BF0B File Offset: 0x0002A10B
			public void Dispose()
			{
				this.DisposeManually(!RuntimeHelpersAbstraction.IsWellKnownNoReferenceContainsType<T>());
			}

			// Token: 0x06000C99 RID: 3225 RVA: 0x0002BF1B File Offset: 0x0002A11B
			public void DisposeManually(bool clearArray)
			{
				if (this.pool != null)
				{
					if (clearArray)
					{
						global::System.Array.Clear(this.Array, 0, this.Length);
					}
					this.pool.Return(this.Array, false);
					this.pool = null;
				}
			}

			// Token: 0x0400065A RID: 1626
			public readonly T[] Array;

			// Token: 0x0400065B RID: 1627
			public readonly int Length;

			// Token: 0x0400065C RID: 1628
			private ArrayPool<T> pool;
		}
	}
}
