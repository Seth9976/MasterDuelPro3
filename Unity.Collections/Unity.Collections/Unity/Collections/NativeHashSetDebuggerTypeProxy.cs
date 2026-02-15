using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x02000097 RID: 151
	internal sealed class NativeHashSetDebuggerTypeProxy<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00017D7C File Offset: 0x00015F7C
		public NativeHashSetDebuggerTypeProxy(NativeHashSet<T> data)
		{
			this.Data = data.m_Data;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00017D90 File Offset: 0x00015F90
		public unsafe List<T> Items
		{
			get
			{
				if (this.Data == null)
				{
					return null;
				}
				List<T> result = new List<T>();
				using (NativeArray<T> items = this.Data->GetKeyArray(Allocator.Temp))
				{
					for (int i = 0; i < items.Length; i++)
					{
						result.Add(items[i]);
					}
				}
				return result;
			}
		}

		// Token: 0x040003BF RID: 959
		private unsafe HashMapHelper<T>* Data;
	}
}
