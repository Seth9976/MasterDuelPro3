using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000110 RID: 272
	internal sealed class UnsafeHashMapDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x000234E5 File Offset: 0x000216E5
		public UnsafeHashMapDebuggerTypeProxy(UnsafeHashMap<TKey, TValue> target)
		{
			this.Data = target.m_Data;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000234F9 File Offset: 0x000216F9
		public UnsafeHashMapDebuggerTypeProxy(UnsafeHashMap<TKey, TValue>.ReadOnly target)
		{
			this.Data = target.m_Data;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00023510 File Offset: 0x00021710
		public List<Pair<TKey, TValue>> Items
		{
			get
			{
				List<Pair<TKey, TValue>> result = new List<Pair<TKey, TValue>>();
				using (NativeKeyValueArrays<TKey, TValue> kva = this.Data.GetKeyValueArrays<TValue>(Allocator.Temp))
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

		// Token: 0x040004C2 RID: 1218
		private HashMapHelper<TKey> Data;
	}
}
