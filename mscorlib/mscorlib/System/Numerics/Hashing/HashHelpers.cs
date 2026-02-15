using System;

namespace System.Numerics.Hashing
{
	// Token: 0x0200068B RID: 1675
	internal static class HashHelpers
	{
		// Token: 0x06003461 RID: 13409 RVA: 0x000C81FB File Offset: 0x000C63FB
		public static int Combine(int h1, int h2)
		{
			return (((h1 << 5) | (int)((uint)h1 >> 27)) + h1) ^ h2;
		}

		// Token: 0x04001B76 RID: 7030
		public static readonly int RandomSeed = new Random().Next(int.MinValue, int.MaxValue);
	}
}
