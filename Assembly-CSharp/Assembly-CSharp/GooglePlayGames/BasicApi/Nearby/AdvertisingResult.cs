using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020011CC RID: 4556
	public struct AdvertisingResult
	{
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x060087AC RID: 34732 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Succeeded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x060087AD RID: 34733 RVA: 0x000029CC File Offset: 0x00000BCC
		public ResponseStatus Status
		{
			get
			{
				return (ResponseStatus)0;
			}
		}

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060087AE RID: 34734 RVA: 0x0000216A File Offset: 0x0000036A
		public string LocalEndpointName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400C226 RID: 49702
		private readonly ResponseStatus mStatus;

		// Token: 0x0400C227 RID: 49703
		private readonly string mLocalEndpointName;
	}
}
