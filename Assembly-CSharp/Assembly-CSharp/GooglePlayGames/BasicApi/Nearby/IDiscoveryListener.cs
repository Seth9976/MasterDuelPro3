using System;

namespace GooglePlayGames.BasicApi.Nearby
{
	// Token: 0x020011D1 RID: 4561
	public interface IDiscoveryListener
	{
		// Token: 0x060087BE RID: 34750
		void OnEndpointFound(EndpointDetails discoveredEndpoint);

		// Token: 0x060087BF RID: 34751
		void OnEndpointLost(string lostEndpointId);
	}
}
