using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020011D4 RID: 4564
	public struct NearbyConnectionConfiguration
	{
		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x060087C2 RID: 34754 RVA: 0x000F1669 File Offset: 0x000EF869
		public long LocalClientId
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060087C3 RID: 34755 RVA: 0x0000216A File Offset: 0x0000036A
		public Action<InitializationStatus> InitializationCallback
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400C23D RID: 49725
		public const int MaxUnreliableMessagePayloadLength = 1168;

		// Token: 0x0400C23E RID: 49726
		public const int MaxReliableMessagePayloadLength = 4096;

		// Token: 0x0400C23F RID: 49727
		private readonly Action<InitializationStatus> mInitializationCallback;

		// Token: 0x0400C240 RID: 49728
		private readonly long mLocalClientId;
	}
}
