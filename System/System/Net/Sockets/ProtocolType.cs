using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the protocols that the <see cref="T:System.Net.Sockets.Socket" /> class supports.</summary>
	// Token: 0x020004B6 RID: 1206
	public enum ProtocolType
	{
		/// <summary>Internet Protocol.</summary>
		// Token: 0x04001488 RID: 5256
		IP,
		/// <summary>IPv6 Hop by Hop Options header.</summary>
		// Token: 0x04001489 RID: 5257
		IPv6HopByHopOptions = 0,
		/// <summary>Internet Control Message Protocol.</summary>
		// Token: 0x0400148A RID: 5258
		Icmp,
		/// <summary>Internet Group Management Protocol.</summary>
		// Token: 0x0400148B RID: 5259
		Igmp,
		/// <summary>Gateway To Gateway Protocol.</summary>
		// Token: 0x0400148C RID: 5260
		Ggp,
		/// <summary>Internet Protocol version 4.</summary>
		// Token: 0x0400148D RID: 5261
		IPv4,
		/// <summary>Transmission Control Protocol.</summary>
		// Token: 0x0400148E RID: 5262
		Tcp = 6,
		/// <summary>PARC Universal Packet Protocol.</summary>
		// Token: 0x0400148F RID: 5263
		Pup = 12,
		/// <summary>User Datagram Protocol.</summary>
		// Token: 0x04001490 RID: 5264
		Udp = 17,
		/// <summary>Internet Datagram Protocol.</summary>
		// Token: 0x04001491 RID: 5265
		Idp = 22,
		/// <summary>Internet Protocol version 6 (IPv6). </summary>
		// Token: 0x04001492 RID: 5266
		IPv6 = 41,
		/// <summary>IPv6 Routing header.</summary>
		// Token: 0x04001493 RID: 5267
		IPv6RoutingHeader = 43,
		/// <summary>IPv6 Fragment header.</summary>
		// Token: 0x04001494 RID: 5268
		IPv6FragmentHeader,
		/// <summary>IPv6 Encapsulating Security Payload header.</summary>
		// Token: 0x04001495 RID: 5269
		IPSecEncapsulatingSecurityPayload = 50,
		/// <summary>IPv6 Authentication header. For details, see RFC 2292 section 2.2.1, available at http://www.ietf.org.</summary>
		// Token: 0x04001496 RID: 5270
		IPSecAuthenticationHeader,
		/// <summary>Internet Control Message Protocol for IPv6.</summary>
		// Token: 0x04001497 RID: 5271
		IcmpV6 = 58,
		/// <summary>IPv6 No next header.</summary>
		// Token: 0x04001498 RID: 5272
		IPv6NoNextHeader,
		/// <summary>IPv6 Destination Options header.</summary>
		// Token: 0x04001499 RID: 5273
		IPv6DestinationOptions,
		/// <summary>Net Disk Protocol (unofficial).</summary>
		// Token: 0x0400149A RID: 5274
		ND = 77,
		/// <summary>Raw IP packet protocol.</summary>
		// Token: 0x0400149B RID: 5275
		Raw = 255,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x0400149C RID: 5276
		Unspecified = 0,
		/// <summary>Internet Packet Exchange Protocol.</summary>
		// Token: 0x0400149D RID: 5277
		Ipx = 1000,
		/// <summary>Sequenced Packet Exchange protocol.</summary>
		// Token: 0x0400149E RID: 5278
		Spx = 1256,
		/// <summary>Sequenced Packet Exchange version 2 protocol.</summary>
		// Token: 0x0400149F RID: 5279
		SpxII,
		/// <summary>Unknown protocol.</summary>
		// Token: 0x040014A0 RID: 5280
		Unknown = -1
	}
}
