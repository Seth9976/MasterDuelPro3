using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000064 RID: 100
	internal static class BurstRuntime
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00004304 File Offset: 0x00002504
		public static long GetHashCode64<T>()
		{
			return BurstRuntime.HashCode64<T>.Value;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000431C File Offset: 0x0000251C
		internal static long HashStringWithFNV1A64(string text)
		{
			ulong result = 14695981039346656037UL;
			foreach (char c in text)
			{
				result = 1099511628211UL * (result ^ (ulong)((byte)(c & 'ÿ')));
				result = 1099511628211UL * (result ^ (ulong)((byte)(c >> 8)));
			}
			return (long)result;
		}

		// Token: 0x02000065 RID: 101
		private struct HashCode64<T>
		{
			// Token: 0x04000110 RID: 272
			public static readonly long Value = BurstRuntime.HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
		}
	}
}
