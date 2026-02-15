using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200004A RID: 74
	public sealed class KeyBuilder
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009E95 File Offset: 0x00008095
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

		// Token: 0x0600018C RID: 396 RVA: 0x00009EB0 File Offset: 0x000080B0
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x040001FB RID: 507
		private static RandomNumberGenerator rng;
	}
}
