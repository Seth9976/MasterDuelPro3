using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000093 RID: 147
	internal sealed class NativeHashMapDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00017A80 File Offset: 0x00015C80
		public NativeHashMapDebuggerTypeProxy(NativeHashMap<TKey, TValue> target)
		{
			this.Data = target.m_Data;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00017A94 File Offset: 0x00015C94
		public NativeHashMapDebuggerTypeProxy(NativeHashMap<TKey, TValue>.ReadOnly target)
		{
			this.Data = target.m_Data;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00017AA8 File Offset: 0x00015CA8
		public unsafe List<Pair<TKey, TValue>> Items
		{
			get
			{
				if (this.Data == null)
				{
					return null;
				}
				List<Pair<TKey, TValue>> result = new List<Pair<TKey, TValue>>();
				using (NativeKeyValueArrays<TKey, TValue> kva = this.Data->GetKeyValueArrays<TValue>(Allocator.Temp))
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

		// Token: 0x040003BB RID: 955
		private unsafe HashMapHelper<TKey>* Data;
	}
}
