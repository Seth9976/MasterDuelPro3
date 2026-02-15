using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000123 RID: 291
	internal sealed class UnsafePtrListDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00024A32 File Offset: 0x00022C32
		public UnsafePtrListDebugView(UnsafePtrList<T> data)
		{
			this.Data = data;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00024A44 File Offset: 0x00022C44
		public unsafe T*[] Items
		{
			get
			{
				T*[] result = new T*[this.Data.Length];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = *(IntPtr*)(this.Data.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*));
				}
				return result;
			}
		}

		// Token: 0x040004E7 RID: 1255
		private UnsafePtrList<T> Data;
	}
}
