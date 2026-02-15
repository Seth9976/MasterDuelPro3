using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x020007E7 RID: 2023
	internal static class MonoLinqHelper
	{
		// Token: 0x0600412D RID: 16685 RVA: 0x000FB9ED File Offset: 0x000F9BED
		public static T[] ToArray<T>(this IEnumerable<T> source)
		{
			return EnumerableHelpers.ToArray<T>(source);
		}
	}
}
