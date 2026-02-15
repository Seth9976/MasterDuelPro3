using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000AC RID: 172
	public static class HashSetExtensions
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x00018D10 File Offset: 0x00016F10
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00018D60 File Offset: 0x00016F60
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000838 RID: 2104 RVA: 0x00018DE4 File Offset: 0x00016FE4
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00018E34 File Offset: 0x00017034
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00018E84 File Offset: 0x00017084
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600083B RID: 2107 RVA: 0x00018F08 File Offset: 0x00017108
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00018F58 File Offset: 0x00017158
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00018FA8 File Offset: 0x000171A8
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600083E RID: 2110 RVA: 0x0001902C File Offset: 0x0001722C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001907C File Offset: 0x0001727C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000190CC File Offset: 0x000172CC
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000841 RID: 2113 RVA: 0x00019150 File Offset: 0x00017350
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000191A0 File Offset: 0x000173A0
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000191F0 File Offset: 0x000173F0
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000844 RID: 2116 RVA: 0x00019274 File Offset: 0x00017474
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000192C4 File Offset: 0x000174C4
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00019314 File Offset: 0x00017514
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000847 RID: 2119 RVA: 0x00019398 File Offset: 0x00017598
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000193E8 File Offset: 0x000175E8
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00019438 File Offset: 0x00017638
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600084A RID: 2122 RVA: 0x000194BC File Offset: 0x000176BC
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001950C File Offset: 0x0001770C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001955C File Offset: 0x0001775C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600084D RID: 2125 RVA: 0x000195E0 File Offset: 0x000177E0
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00019630 File Offset: 0x00017830
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00019680 File Offset: 0x00017880
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000850 RID: 2128 RVA: 0x00019704 File Offset: 0x00017904
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00019754 File Offset: 0x00017954
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000197A4 File Offset: 0x000179A4
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000853 RID: 2131 RVA: 0x00019828 File Offset: 0x00017A28
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00019878 File Offset: 0x00017A78
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000198C8 File Offset: 0x00017AC8
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000856 RID: 2134 RVA: 0x0001994C File Offset: 0x00017B4C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001999C File Offset: 0x00017B9C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000199EC File Offset: 0x00017BEC
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000859 RID: 2137 RVA: 0x00019A70 File Offset: 0x00017C70
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList128Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00019AC0 File Offset: 0x00017CC0
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00019B10 File Offset: 0x00017D10
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600085C RID: 2140 RVA: 0x00019B94 File Offset: 0x00017D94
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList32Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00019C34 File Offset: 0x00017E34
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600085F RID: 2143 RVA: 0x00019CB8 File Offset: 0x00017EB8
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList4096Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00019D08 File Offset: 0x00017F08
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00019D58 File Offset: 0x00017F58
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000862 RID: 2146 RVA: 0x00019DDC File Offset: 0x00017FDC
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList512Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00019E2C File Offset: 0x0001802C
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00019E7C File Offset: 0x0001807C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000865 RID: 2149 RVA: 0x00019F00 File Offset: 0x00018100
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, FixedList64Bytes<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00019F50 File Offset: 0x00018150
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00019FA0 File Offset: 0x000181A0
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000868 RID: 2152 RVA: 0x0001A024 File Offset: 0x00018224
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001A074 File Offset: 0x00018274
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001A0C4 File Offset: 0x000182C4
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600086B RID: 2155 RVA: 0x0001A148 File Offset: 0x00018348
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001A198 File Offset: 0x00018398
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001A1E8 File Offset: 0x000183E8
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600086E RID: 2158 RVA: 0x0001A26C File Offset: 0x0001846C
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001A2BC File Offset: 0x000184BC
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001A30C File Offset: 0x0001850C
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000871 RID: 2161 RVA: 0x0001A390 File Offset: 0x00018590
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001A3E0 File Offset: 0x000185E0
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001A430 File Offset: 0x00018630
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000874 RID: 2164 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeParallelHashSet<T>.ReadOnly other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001A504 File Offset: 0x00018704
		public static void ExceptWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Remove(item);
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001A554 File Offset: 0x00018754
		public static void IntersectWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000877 RID: 2167 RVA: 0x0001A5D8 File Offset: 0x000187D8
		public static void UnionWith<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeParallelHashSet<T> container, NativeList<T> other) where T : struct, ValueType, IEquatable<T>
		{
			foreach (T item in other)
			{
				container.Add(item);
			}
		}
	}
}
