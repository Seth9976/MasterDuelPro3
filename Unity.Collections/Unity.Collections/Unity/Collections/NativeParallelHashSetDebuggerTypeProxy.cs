using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000AB RID: 171
	internal sealed class NativeParallelHashSetDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x00018C99 File Offset: 0x00016E99
		public NativeParallelHashSetDebuggerTypeProxy(NativeParallelHashSet<T> data)
		{
			this.Data = data;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00018CA8 File Offset: 0x00016EA8
		public List<T> Items
		{
			get
			{
				List<T> result = new List<T>();
				using (NativeArray<T> keys = this.Data.ToNativeArray(Allocator.Temp))
				{
					for (int i = 0; i < keys.Length; i++)
					{
						result.Add(keys[i]);
					}
				}
				return result;
			}
		}

		// Token: 0x040003D0 RID: 976
		private NativeParallelHashSet<T> Data;
	}
}
