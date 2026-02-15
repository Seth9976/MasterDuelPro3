using System;

namespace System.Numerics.Hashing
{
	// Token: 0x02000006 RID: 6
	internal static class HashHelpers
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002074 File Offset: 0x00000274
		public static int Combine(int h1, int h2)
		{
			return (((h1 << 5) | (int)((uint)h1 >> 27)) + h1) ^ h2;
		}

		// Token: 0x04000002 RID: 2
		public static readonly int RandomSeed = Guid.NewGuid().GetHashCode();
	}
}
