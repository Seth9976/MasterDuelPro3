using System;
using System.Collections.Generic;

namespace YgomSystem.Extension
{
	// Token: 0x0200076F RID: 1903
	public static class IReadOnlyCollectionExtension
	{
		// Token: 0x06003B4C RID: 15180 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> self)
		{
			return false;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsExists<T>(this IReadOnlyCollection<T> self)
		{
			return false;
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int SafeGetCount<T>(this IReadOnlyCollection<T> self)
		{
			return 0;
		}
	}
}
