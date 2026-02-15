using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000114 RID: 276
	internal sealed class UnsafeHashSetDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x00023811 File Offset: 0x00021A11
		public UnsafeHashSetDebuggerTypeProxy(UnsafeHashSet<T> data)
		{
			this.Data = data.m_Data;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00023828 File Offset: 0x00021A28
		public List<T> Items
		{
			get
			{
				List<T> result = new List<T>();
				using (NativeArray<T> keys = this.Data.GetKeyArray(Allocator.Temp))
				{
					for (int i = 0; i < keys.Length; i++)
					{
						result.Add(keys[i]);
					}
				}
				return result;
			}
		}

		// Token: 0x040004C6 RID: 1222
		private HashMapHelper<T> Data;
	}
}
