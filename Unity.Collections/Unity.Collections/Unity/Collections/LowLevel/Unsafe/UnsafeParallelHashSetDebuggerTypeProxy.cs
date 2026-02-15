using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000136 RID: 310
	internal sealed class UnsafeParallelHashSetDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002627D File Offset: 0x0002447D
		public UnsafeParallelHashSetDebuggerTypeProxy(UnsafeParallelHashSet<T> data)
		{
			this.Data = data;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002628C File Offset: 0x0002448C
		public List<T> Items
		{
			get
			{
				List<T> result = new List<T>();
				using (NativeArray<T> item = this.Data.ToNativeArray(Allocator.Temp))
				{
					for (int i = 0; i < item.Length; i++)
					{
						result.Add(item[i]);
					}
				}
				return result;
			}
		}

		// Token: 0x04000511 RID: 1297
		private UnsafeParallelHashSet<T> Data;
	}
}
