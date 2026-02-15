using System;

namespace System.Text
{
	// Token: 0x020002F7 RID: 759
	internal static class StringBuilderCache
	{
		// Token: 0x06001B16 RID: 6934 RVA: 0x000673BC File Offset: 0x000655BC
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

		// Token: 0x06001B17 RID: 6935 RVA: 0x000673F8 File Offset: 0x000655F8
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.t_cachedInstance = sb;
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0006740D File Offset: 0x0006560D
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string text = sb.ToString();
			StringBuilderCache.Release(sb);
			return text;
		}

		// Token: 0x04000C9D RID: 3229
		[ThreadStatic]
		private static StringBuilder t_cachedInstance;
	}
}
