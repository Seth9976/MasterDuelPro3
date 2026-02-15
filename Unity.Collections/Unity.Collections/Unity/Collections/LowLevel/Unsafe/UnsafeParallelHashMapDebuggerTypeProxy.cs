using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000130 RID: 304
	internal sealed class UnsafeParallelHashMapDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] TKey, [global::System.Runtime.CompilerServices.IsUnmanaged] TValue> where TKey : struct, ValueType, IEquatable<TKey> where TValue : struct, ValueType
	{
		// Token: 0x06000C90 RID: 3216 RVA: 0x00025FBE File Offset: 0x000241BE
		public UnsafeParallelHashMapDebuggerTypeProxy(UnsafeParallelHashMap<TKey, TValue> target)
		{
			this.m_Target = target;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00025FD0 File Offset: 0x000241D0
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

		// Token: 0x0400050A RID: 1290
		private UnsafeParallelHashMap<TKey, TValue> m_Target;
	}
}
