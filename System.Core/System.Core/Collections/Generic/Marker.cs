using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000157 RID: 343
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct Marker
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0002C628 File Offset: 0x0002A828
		public Marker(int count, int index)
		{
			this.Count = count;
			this.Index = index;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0002C638 File Offset: 0x0002A838
		public int Count { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002C640 File Offset: 0x0002A840
		public int Index { get; }
	}
}
