using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011D RID: 285
	internal sealed class UnsafeListTDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x00024542 File Offset: 0x00022742
		public UnsafeListTDebugView(UnsafeList<T> data)
		{
			this.Data = data;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00024554 File Offset: 0x00022754
		public unsafe T[] Items
		{
			get
			{
				T[] result = new T[this.Data.Length];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = this.Data.Ptr[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
				return result;
			}
		}

		// Token: 0x040004DB RID: 1243
		private UnsafeList<T> Data;
	}
}
