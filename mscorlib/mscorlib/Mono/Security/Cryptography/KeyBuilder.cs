using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200006C RID: 108
	internal sealed class KeyBuilder
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000A705 File Offset: 0x00008905
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (KeyBuilder.rng == null)
				{
					KeyBuilder.rng = RandomNumberGenerator.Create();
				}
				return KeyBuilder.rng;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000A720 File Offset: 0x00008920
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A740 File Offset: 0x00008940
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x040001F2 RID: 498
		private static RandomNumberGenerator rng;
	}
}
