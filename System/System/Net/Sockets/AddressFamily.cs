using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the addressing scheme that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020004B1 RID: 1201
	public enum AddressFamily
	{
		/// <summary>Unknown address family.</summary>
		// Token: 0x0400143E RID: 5182
		Unknown = -1,
		/// <summary>Unspecified address family.</summary>
		// Token: 0x0400143F RID: 5183
		Unspecified,
		/// <summary>Unix local to host address.</summary>
		// Token: 0x04001440 RID: 5184
		Unix,
		/// <summary>Address for IP version 4.</summary>
		// Token: 0x04001441 RID: 5185
		InterNetwork,
		/// <summary>ARPANET IMP address.</summary>
		// Token: 0x04001442 RID: 5186
		ImpLink,
		/// <summary>Address for PUP protocols.</summary>
		// Token: 0x04001443 RID: 5187
		Pup,
		/// <summary>Address for MIT CHAOS protocols.</summary>
		// Token: 0x04001444 RID: 5188
		Chaos,
		/// <summary>Address for Xerox NS protocols.</summary>
		// Token: 0x04001445 RID: 5189
		NS,
		/// <summary>IPX or SPX address.</summary>
		// Token: 0x04001446 RID: 5190
		Ipx = 6,
		/// <summary>Address for ISO protocols.</summary>
		// Token: 0x04001447 RID: 5191
		Iso,
		/// <summary>Address for OSI protocols.</summary>
		// Token: 0x04001448 RID: 5192
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) address.</summary>
		// Token: 0x04001449 RID: 5193
		Ecma,
		/// <summary>Address for Datakit protocols.</summary>
		// Token: 0x0400144A RID: 5194
		DataKit,
		/// <summary>Addresses for CCITT protocols, such as X.25.</summary>
		// Token: 0x0400144B RID: 5195
		Ccitt,
		/// <summary>IBM SNA address.</summary>
		// Token: 0x0400144C RID: 5196
		Sna,
		/// <summary>DECnet address.</summary>
		// Token: 0x0400144D RID: 5197
		DecNet,
		/// <summary>Direct data-link interface address.</summary>
		// Token: 0x0400144E RID: 5198
		DataLink,
		/// <summary>LAT address.</summary>
		// Token: 0x0400144F RID: 5199
		Lat,
		/// <summary>NSC Hyperchannel address.</summary>
		// Token: 0x04001450 RID: 5200
		HyperChannel,
		/// <summary>AppleTalk address.</summary>
		// Token: 0x04001451 RID: 5201
		AppleTalk,
		/// <summary>NetBios address.</summary>
		// Token: 0x04001452 RID: 5202
		NetBios,
		/// <summary>VoiceView address.</summary>
		// Token: 0x04001453 RID: 5203
		VoiceView,
		/// <summary>FireFox address.</summary>
		// Token: 0x04001454 RID: 5204
		FireFox,
		/// <summary>Banyan address.</summary>
		// Token: 0x04001455 RID: 5205
		Banyan = 21,
		/// <summary>Native ATM services address.</summary>
		// Token: 0x04001456 RID: 5206
		Atm,
		/// <summary>Address for IP version 6.</summary>
		// Token: 0x04001457 RID: 5207
		InterNetworkV6,
		/// <summary>Address for Microsoft cluster products.</summary>
		// Token: 0x04001458 RID: 5208
		Cluster,
		/// <summary>IEEE 1284.4 workgroup address.</summary>
		// Token: 0x04001459 RID: 5209
		Ieee12844,
		/// <summary>IrDA address.</summary>
		// Token: 0x0400145A RID: 5210
		Irda,
		/// <summary>Address for Network Designers OSI gateway-enabled protocols.</summary>
		// Token: 0x0400145B RID: 5211
		NetworkDesigners = 28,
		/// <summary>MAX address.</summary>
		// Token: 0x0400145C RID: 5212
		Max
	}
}
