using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000B3 RID: 179
	internal sealed class NativeParallelMultiHashMapDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0001AA39 File Offset: 0x00018C39
		public NativeParallelMultiHashMapDebuggerTypeProxy(NativeParallelMultiHashMap<TKey, TValue> target)
		{
			this.m_Target = target;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001AA48 File Offset: 0x00018C48
		public List<ListPair<TKey, List<TValue>>> Items
		{
			get
			{
				List<ListPair<TKey, List<TValue>>> result = new List<ListPair<TKey, List<TValue>>>();
				ValueTuple<NativeArray<TKey>, int> keys = default(ValueTuple<NativeArray<TKey>, int>);
				using (NativeParallelHashMap<TKey, TValue> uniques = new NativeParallelHashMap<TKey, TValue>(this.m_Target.Count(), Allocator.Temp))
				{
					foreach (KeyValue<TKey, TValue> keyValue in this.m_Target)
					{
						uniques.TryAdd(keyValue.Key, default(TValue));
					}
					keys.Item1 = uniques.GetKeyArray(Allocator.Temp);
					keys.Item2 = keys.Item1.Length;
				}
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

		// Token: 0x040003DD RID: 989
		private NativeParallelMultiHashMap<TKey, TValue> m_Target;
	}
}
