using System;
using System.Text;

namespace System.Collections.Generic
{
	// Token: 0x02000753 RID: 1875
	public static class KeyValuePair
	{
		// Token: 0x06003BA4 RID: 15268 RVA: 0x000E6A04 File Offset: 0x000E4C04
		internal static string PairToString(object key, object value)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			stringBuilder.Append('[');
			if (key != null)
			{
				stringBuilder.Append(key);
			}
			stringBuilder.Append(", ");
			if (value != null)
			{
				stringBuilder.Append(value);
			}
			stringBuilder.Append(']');
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}
	}
}
