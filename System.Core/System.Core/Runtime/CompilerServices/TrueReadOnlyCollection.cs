using System;
using System.Collections.ObjectModel;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200011B RID: 283
	internal sealed class TrueReadOnlyCollection<T> : ReadOnlyCollection<T>
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x00026021 File Offset: 0x00024221
		public TrueReadOnlyCollection(params T[] list)
			: base(list)
		{
		}
	}
}
