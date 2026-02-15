using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000137 RID: 311
	public static class HashSetExtensions
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x000262F4 File Offset: 0x000244F4
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00026344 File Offset: 0x00024544
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000263C8 File Offset: 0x000245C8
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00026418 File Offset: 0x00024618
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00026468 File Offset: 0x00024668
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000264EC File Offset: 0x000246EC
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002653C File Offset: 0x0002473C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002658C File Offset: 0x0002478C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00026610 File Offset: 0x00024810
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00026660 File Offset: 0x00024860
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000266B0 File Offset: 0x000248B0
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00026734 File Offset: 0x00024934
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00026784 File Offset: 0x00024984
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000267D4 File Offset: 0x000249D4
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00026858 File Offset: 0x00024A58
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000268A8 File Offset: 0x00024AA8
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000268F8 File Offset: 0x00024AF8
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002697C File Offset: 0x00024B7C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000269CC File Offset: 0x00024BCC
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00026A1C File Offset: 0x00024C1C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00026AA0 File Offset: 0x00024CA0
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00026AF0 File Offset: 0x00024CF0
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00026B40 File Offset: 0x00024D40
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00026BC4 File Offset: 0x00024DC4
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00026C14 File Offset: 0x00024E14
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00026C64 File Offset: 0x00024E64
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00026CE8 File Offset: 0x00024EE8
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00026D38 File Offset: 0x00024F38
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00026D88 File Offset: 0x00024F88
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00026E0C File Offset: 0x0002500C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00026E5C File Offset: 0x0002505C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00026EAC File Offset: 0x000250AC
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00026F30 File Offset: 0x00025130
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00026F80 File Offset: 0x00025180
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00026FD0 File Offset: 0x000251D0
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00027054 File Offset: 0x00025254
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000270A4 File Offset: 0x000252A4
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000270F4 File Offset: 0x000252F4
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00027178 File Offset: 0x00025378
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000271C8 File Offset: 0x000253C8
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00027218 File Offset: 0x00025418
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002729C File Offset: 0x0002549C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000272EC File Offset: 0x000254EC
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002733C File Offset: 0x0002553C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x000273C0 File Offset: 0x000255C0
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00027410 File Offset: 0x00025610
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00027460 File Offset: 0x00025660
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000274E4 File Offset: 0x000256E4
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00027534 File Offset: 0x00025734
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00027584 File Offset: 0x00025784
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00027608 File Offset: 0x00025808
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00027658 File Offset: 0x00025858
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000276A8 File Offset: 0x000258A8
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002772C File Offset: 0x0002592C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002777C File Offset: 0x0002597C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000277CC File Offset: 0x000259CC
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00027850 File Offset: 0x00025A50
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000278A0 File Offset: 0x00025AA0
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000278F0 File Offset: 0x00025AF0
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00027974 File Offset: 0x00025B74
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000279C4 File Offset: 0x00025BC4
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00027A14 File Offset: 0x00025C14
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00027A98 File Offset: 0x00025C98
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00027AE8 File Offset: 0x00025CE8
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00027B38 File Offset: 0x00025D38
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00027BBC File Offset: 0x00025DBC
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00027C0C File Offset: 0x00025E0C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00027C5C File Offset: 0x00025E5C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00027CE0 File Offset: 0x00025EE0
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00027D30 File Offset: 0x00025F30
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00027D80 File Offset: 0x00025F80
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00027E04 File Offset: 0x00026004
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00027E54 File Offset: 0x00026054
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00027EA4 File Offset: 0x000260A4
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00027F28 File Offset: 0x00026128
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00027F78 File Offset: 0x00026178
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00027FC8 File Offset: 0x000261C8
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002804C File Offset: 0x0002624C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002809C File Offset: 0x0002629C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000280EC File Offset: 0x000262EC
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00028170 File Offset: 0x00026370
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000281C0 File Offset: 0x000263C0
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00028210 File Offset: 0x00026410
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00028294 File Offset: 0x00026494
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000282E4 File Offset: 0x000264E4
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00028334 File Offset: 0x00026534
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000283B8 File Offset: 0x000265B8
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00028408 File Offset: 0x00026608
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00028458 File Offset: 0x00026658
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000284DC File Offset: 0x000266DC
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002852C File Offset: 0x0002672C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002857C File Offset: 0x0002677C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00028600 File Offset: 0x00026800
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00028650 File Offset: 0x00026850
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000286A0 File Offset: 0x000268A0
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00028724 File Offset: 0x00026924
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00028774 File Offset: 0x00026974
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000287C4 File Offset: 0x000269C4
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00028848 File Offset: 0x00026A48
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00028898 File Offset: 0x00026A98
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000288E8 File Offset: 0x00026AE8
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002896C File Offset: 0x00026B6C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000289BC File Offset: 0x00026BBC
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00028A0C File Offset: 0x00026C0C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00028A90 File Offset: 0x00026C90
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00028B30 File Offset: 0x00026D30
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00028BB4 File Offset: 0x00026DB4
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00028C04 File Offset: 0x00026E04
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00028C54 File Offset: 0x00026E54
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00028CD8 File Offset: 0x00026ED8
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00028D28 File Offset: 0x00026F28
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00028D78 File Offset: 0x00026F78
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00028DFC File Offset: 0x00026FFC
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00028E4C File Offset: 0x0002704C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00028E9C File Offset: 0x0002709C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00028F20 File Offset: 0x00027120
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00028F70 File Offset: 0x00027170
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00028FC0 File Offset: 0x000271C0
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00029044 File Offset: 0x00027244
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00029094 File Offset: 0x00027294
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000290E4 File Offset: 0x000272E4
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00029168 File Offset: 0x00027368
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000291B8 File Offset: 0x000273B8
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00029208 File Offset: 0x00027408
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			UnsafeList<T> result = new UnsafeList<T>(container.Count(), Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			foreach (T item in other)
			{
				if (container.Contains(item))
				{
					result.Add(in item);
				}
			}
			container.Clear();
			(ref container).UnionWith(result);
			result.Dispose();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0002928C File Offset: 0x0002748C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeParallelHashSet<T> container, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}
	}
}
