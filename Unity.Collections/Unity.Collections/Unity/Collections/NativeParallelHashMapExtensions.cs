using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000A6 RID: 166
	[GenerateTestsForBurstCompatibility]
	public static class NativeParallelHashMapExtensions
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00018988 File Offset: 0x00016B88
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[] { typeof(int) })]
		public static int Unique<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeArray<T> array) where T : struct, ValueType, IEquatable<T>
		{
			if (array.Length == 0)
			{
				return 0;
			}
			int first = 0;
			int last = array.Length;
			int result = first;
			while (++first != last)
			{
				T t = array[result];
				if (!t.Equals(array[first]))
				{
					array[++result] = array[first];
				}
			}
			return result + 1;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000189F0 File Offset: 0x00016BF0
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArray<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(this UnsafeParallelMultiHashMap<TKey, TValue> container, AllocatorManager.AllocatorHandle allocator) where TKey : struct, ValueType, IEquatable<TKey>, IComparable<TKey> where TValue : struct, ValueType
		{
			NativeArray<TKey> keyArray = container.GetKeyArray(allocator);
			keyArray.Sort<TKey>();
			int uniques = keyArray.Unique<TKey>();
			return new ValueTuple<NativeArray<TKey>, int>(keyArray, uniques);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00018A18 File Offset: 0x00016C18
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArray<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(this NativeParallelMultiHashMap<TKey, TValue> container, AllocatorManager.AllocatorHandle allocator) where TKey : struct, ValueType, IEquatable<TKey>, IComparable<TKey> where TValue : struct, ValueType
		{
			NativeArray<TKey> keyArray = container.GetKeyArray(allocator);
			keyArray.Sort<TKey>();
			int uniques = keyArray.Unique<TKey>();
			return new ValueTuple<NativeArray<TKey>, int>(keyArray, uniques);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00018A40 File Offset: 0x00016C40
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static UnsafeParallelHashMapBucketData GetUnsafeBucketData<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(this NativeParallelHashMap<TKey, TValue> container) where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
		{
			return container.m_HashMapData.m_Buffer->GetBucketData();
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00018A52 File Offset: 0x00016C52
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static UnsafeParallelHashMapBucketData GetUnsafeBucketData<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(this NativeParallelMultiHashMap<TKey, TValue> container) where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
		{
			return container.m_MultiHashMapData.m_Buffer->GetBucketData();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00018A64 File Offset: 0x00016C64
		[GenerateTestsForBurstCompatibility(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static void Remove<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue>(this NativeParallelMultiHashMap<TKey, TValue> container, TKey key, TValue value) where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType, IEquatable<TValue>
		{
			container.m_MultiHashMapData.Remove<TValue>(key, value);
		}
	}
}
