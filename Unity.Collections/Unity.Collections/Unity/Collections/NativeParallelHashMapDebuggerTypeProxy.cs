using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000A5 RID: 165
	internal sealed class NativeParallelHashMapDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x000188DA File Offset: 0x00016ADA
		public NativeParallelHashMapDebuggerTypeProxy(NativeParallelHashMap<TKey, TValue> target)
		{
			this.m_Target = target.m_HashMapData;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000188EE File Offset: 0x00016AEE
		internal NativeParallelHashMapDebuggerTypeProxy(NativeParallelHashMap<TKey, TValue>.ReadOnly target)
		{
			this.m_Target = target.m_HashMapData;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00018904 File Offset: 0x00016B04
		public List<Pair<TKey, TValue>> Items
		{
			get
			{
				List<Pair<TKey, TValue>> result = new List<Pair<TKey, TValue>>();
				using (NativeKeyValueArrays<TKey, TValue> kva = this.m_Target.GetKeyValueArrays(Allocator.Temp))
				{
					for (int i = 0; i < kva.Length; i++)
					{
						List<Pair<TKey, TValue>> list = result;
						NativeArray<TKey> keys = kva.Keys;
						TKey tkey = keys[i];
						NativeArray<TValue> values = kva.Values;
						list.Add(new Pair<TKey, TValue>(tkey, values[i]));
					}
				}
				return result;
			}
		}

		// Token: 0x040003CB RID: 971
		private UnsafeParallelHashMap<TKey, TValue> m_Target;
	}
}
