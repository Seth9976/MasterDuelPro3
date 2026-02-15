using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020011CD RID: 4557
	public struct ConnectionRequest
	{
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x060087AF RID: 34735 RVA: 0x000F7610 File Offset: 0x000F5810
		public EndpointDetails RemoteEndpoint
		{
			get
			{
				return default(EndpointDetails);
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x060087B0 RID: 34736 RVA: 0x0000216A File Offset: 0x0000036A
		public byte[] Payload
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400C228 RID: 49704
		private readonly EndpointDetails mRemoteEndpoint;

		// Token: 0x0400C229 RID: 49705
		private readonly byte[] mPayload;
	}
}
