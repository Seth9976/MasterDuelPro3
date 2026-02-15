using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020011D2 RID: 4562
	public interface IMessageListener
	{
		// Token: 0x060087C0 RID: 34752
		void OnMessageReceived(string remoteEndpointId, byte[] data, bool isReliableMessage);

		// Token: 0x060087C1 RID: 34753
		void OnRemoteEndpointDisconnected(string remoteEndpointId);
	}
}
