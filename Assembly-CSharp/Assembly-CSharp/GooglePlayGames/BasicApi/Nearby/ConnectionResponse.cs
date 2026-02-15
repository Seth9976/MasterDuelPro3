using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020011CE RID: 4558
	public struct ConnectionResponse
	{
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x060087B1 RID: 34737 RVA: 0x000F1669 File Offset: 0x000EF869
		public long LocalClientId
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060087B2 RID: 34738 RVA: 0x0000216A File Offset: 0x0000036A
		public string RemoteEndpointId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060087B3 RID: 34739 RVA: 0x000029CC File Offset: 0x00000BCC
		public ConnectionResponse.Status ResponseStatus
		{
			get
			{
				return ConnectionResponse.Status.Accepted;
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060087B4 RID: 34740 RVA: 0x0000216A File Offset: 0x0000036A
		public byte[] Payload
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060087B5 RID: 34741 RVA: 0x000F7628 File Offset: 0x000F5828
		public static ConnectionResponse Rejected(long localClientId, string remoteEndpointId)
		{
			return default(ConnectionResponse);
		}

		// Token: 0x060087B6 RID: 34742 RVA: 0x000F7640 File Offset: 0x000F5840
		public static ConnectionResponse NetworkNotConnected(long localClientId, string remoteEndpointId)
		{
			return default(ConnectionResponse);
		}

		// Token: 0x060087B7 RID: 34743 RVA: 0x000F7658 File Offset: 0x000F5858
		public static ConnectionResponse InternalError(long localClientId, string remoteEndpointId)
		{
			return default(ConnectionResponse);
		}

		// Token: 0x060087B8 RID: 34744 RVA: 0x000F7670 File Offset: 0x000F5870
		public static ConnectionResponse EndpointNotConnected(long localClientId, string remoteEndpointId)
		{
			return default(ConnectionResponse);
		}

		// Token: 0x060087B9 RID: 34745 RVA: 0x000F7688 File Offset: 0x000F5888
		public static ConnectionResponse Accepted(long localClientId, string remoteEndpointId, byte[] payload)
		{
			return default(ConnectionResponse);
		}

		// Token: 0x060087BA RID: 34746 RVA: 0x000F76A0 File Offset: 0x000F58A0
		public static ConnectionResponse AlreadyConnected(long localClientId, string remoteEndpointId)
		{
			return default(ConnectionResponse);
		}

		// Token: 0x0400C22A RID: 49706
		private static readonly byte[] EmptyPayload;

		// Token: 0x0400C22B RID: 49707
		private readonly long mLocalClientId;

		// Token: 0x0400C22C RID: 49708
		private readonly string mRemoteEndpointId;

		// Token: 0x0400C22D RID: 49709
		private readonly ConnectionResponse.Status mResponseStatus;

		// Token: 0x0400C22E RID: 49710
		private readonly byte[] mPayload;

		// Token: 0x020011CF RID: 4559
		public enum Status
		{
			// Token: 0x0400C230 RID: 49712
			Accepted,
			// Token: 0x0400C231 RID: 49713
			Rejected,
			// Token: 0x0400C232 RID: 49714
			ErrorInternal,
			// Token: 0x0400C233 RID: 49715
			ErrorNetworkNotConnected,
			// Token: 0x0400C234 RID: 49716
			ErrorEndpointNotConnected,
			// Token: 0x0400C235 RID: 49717
			ErrorAlreadyConnected
		}
	}
}
