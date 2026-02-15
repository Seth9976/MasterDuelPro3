using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x02000147 RID: 327
	internal static class EmptyReadOnlyCollection<T>
	{
		// Token: 0x0400034E RID: 846
		public static readonly ReadOnlyCollection<T> Instance = new TrueReadOnlyCollection<T>(Array.Empty<T>());
	}
}
