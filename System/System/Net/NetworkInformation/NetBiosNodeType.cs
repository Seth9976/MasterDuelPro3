using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the Network Basic Input/Output System (NetBIOS) node type.</summary>
	// Token: 0x02000460 RID: 1120
	public enum NetBiosNodeType
	{
		/// <summary>An unknown node type.</summary>
		// Token: 0x04001301 RID: 4865
		Unknown,
		/// <summary>A broadcast node.</summary>
		// Token: 0x04001302 RID: 4866
		Broadcast,
		/// <summary>A peer-to-peer node.</summary>
		// Token: 0x04001303 RID: 4867
		Peer2Peer,
		/// <summary>A mixed node.</summary>
		// Token: 0x04001304 RID: 4868
		Mixed = 4,
		/// <summary>A hybrid node.</summary>
		// Token: 0x04001305 RID: 4869
		Hybrid = 8
	}
}
