using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000188 RID: 392
	internal ref struct ByReference<T>
	{
		// Token: 0x06000E83 RID: 3715 RVA: 0x000339FF File Offset: 0x00031BFF
		[Intrinsic]
		public ByReference(ref T value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x000339FF File Offset: 0x00031BFF
		public ref T Value
		{
			[Intrinsic]
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040005B7 RID: 1463
		private IntPtr _value;
	}
}
