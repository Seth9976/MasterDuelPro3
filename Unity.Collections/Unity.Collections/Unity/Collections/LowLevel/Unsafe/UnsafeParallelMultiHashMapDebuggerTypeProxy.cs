using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013D RID: 317
	internal sealed class UnsafeParallelMultiHashMapDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey>, IComparable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x00029745 File Offset: 0x00027945
		public UnsafeParallelMultiHashMapDebuggerTypeProxy(UnsafeParallelMultiHashMap<TKey, TValue> target)
		{
			this.m_Target = target;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00029754 File Offset: 0x00027954
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArray(ref UnsafeParallelMultiHashMap<TKey, TValue> hashMap, AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> keyArray = hashMap.GetKeyArray(allocator);
			keyArray.Sort<TKey>();
			int uniques = keyArray.Unique<TKey>();
			return new ValueTuple<NativeArray<TKey>, int>(keyArray, uniques);
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0002977C File Offset: 0x0002797C
		public List<ListPair<TKey, List<TValue>>> Items
		{
			get
			{
				List<ListPair<TKey, List<TValue>>> result = new List<ListPair<TKey, List<TValue>>>();
				ValueTuple<NativeArray<TKey>, int> keys = UnsafeParallelMultiHashMapDebuggerTypeProxy<TKey, TValue>.GetUniqueKeyArray(ref this.m_Target, Allocator.Temp);
				using (keys.Item1)
				{
					for (int i = 0; i < keys.Item2; i++)
					{
						List<TValue> values = new List<TValue>();
						TValue value;
						NativeParallelMultiHashMapIterator<TKey> iterator;
						if (this.m_Target.TryGetFirstValue(keys.Item1[i], out value, out iterator))
						{
							do
							{
								values.Add(value);
							}
							while (this.m_Target.TryGetNextValue(out value, ref iterator));
						}
						result.Add(new ListPair<TKey, List<TValue>>(keys.Item1[i], values));
					}
				}
				return result;
			}
		}

		// Token: 0x0400051D RID: 1309
		private UnsafeParallelMultiHashMap<TKey, TValue> m_Target;
	}
}
