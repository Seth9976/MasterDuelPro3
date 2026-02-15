using System;

namespace System.Text
{
	// Token: 0x02000025 RID: 37
	internal static class StringBuilderCache
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00005438 File Offset: 0x00003638
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder stringBuilder = StringBuilderCache.t_cachedInstance;
				if (stringBuilder != null && capacity <= stringBuilder.Capacity)
				{
					StringBuilderCache.t_cachedInstance = null;
					stringBuilder.Clear();
					return stringBuilder;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005474 File Offset: 0x00003674
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.t_cachedInstance = sb;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005489 File Offset: 0x00003689
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string text = sb.ToString();
			StringBuilderCache.Release(sb);
			return text;
		}

		// Token: 0x0400005E RID: 94
		[ThreadStatic]
		private static StringBuilder t_cachedInstance;
	}
}
