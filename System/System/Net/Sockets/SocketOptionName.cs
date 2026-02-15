using System;

namespace System.Net.Sockets
{
	/// <summary>Defines configuration option names.</summary>
	// Token: 0x020004BC RID: 1212
	public enum SocketOptionName
	{
		/// <summary>Record debugging information.</summary>
		// Token: 0x040014F2 RID: 5362
		Debug = 1,
		/// <summary>The socket is listening.</summary>
		// Token: 0x040014F3 RID: 5363
		AcceptConnection,
		/// <summary>Allows the socket to be bound to an address that is already in use.</summary>
		// Token: 0x040014F4 RID: 5364
		ReuseAddress = 4,
		/// <summary>Use keep-alives.</summary>
		// Token: 0x040014F5 RID: 5365
		KeepAlive = 8,
		/// <summary>Do not route; send the packet directly to the interface addresses.</summary>
		// Token: 0x040014F6 RID: 5366
		DontRoute = 16,
		/// <summary>Permit sending broadcast messages on the socket.</summary>
		// Token: 0x040014F7 RID: 5367
		Broadcast = 32,
		/// <summary>Bypass hardware when possible.</summary>
		// Token: 0x040014F8 RID: 5368
		UseLoopback = 64,
		/// <summary>Linger on close if unsent data is present.</summary>
		// Token: 0x040014F9 RID: 5369
		Linger = 128,
		/// <summary>Receives out-of-band data in the normal data stream.</summary>
		// Token: 0x040014FA RID: 5370
		OutOfBandInline = 256,
		/// <summary>Close the socket gracefully without lingering.</summary>
		// Token: 0x040014FB RID: 5371
		DontLinger = -129,
		/// <summary>Enables a socket to be bound for exclusive access.</summary>
		// Token: 0x040014FC RID: 5372
		ExclusiveAddressUse = -5,
		/// <summary>Specifies the total per-socket buffer space reserved for sends. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x040014FD RID: 5373
		SendBuffer = 4097,
		/// <summary>Specifies the total per-socket buffer space reserved for receives. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x040014FE RID: 5374
		ReceiveBuffer,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Send" /> operations.</summary>
		// Token: 0x040014FF RID: 5375
		SendLowWater,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operations.</summary>
		// Token: 0x04001500 RID: 5376
		ReceiveLowWater,
		/// <summary>Send a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x04001501 RID: 5377
		SendTimeout,
		/// <summary>Receive a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x04001502 RID: 5378
		ReceiveTimeout,
		/// <summary>Get the error status and clear.</summary>
		// Token: 0x04001503 RID: 5379
		Error,
		/// <summary>Get the socket type.</summary>
		// Token: 0x04001504 RID: 5380
		Type,
		// Token: 0x04001505 RID: 5381
		ReuseUnicastPort = 12295,
		/// <summary>Not supported; will throw a <see cref="T:System.Net.Sockets.SocketException" /> if used.</summary>
		// Token: 0x04001506 RID: 5382
		MaxConnections = 2147483647,
		/// <summary>Specifies the IP options to be inserted into outgoing datagrams.</summary>
		// Token: 0x04001507 RID: 5383
		IPOptions = 1,
		/// <summary>Indicates that the application provides the IP header for outgoing datagrams.</summary>
		// Token: 0x04001508 RID: 5384
		HeaderIncluded,
		/// <summary>Change the IP header type of the service field.</summary>
		// Token: 0x04001509 RID: 5385
		TypeOfService,
		/// <summary>Set the IP header Time-to-Live field.</summary>
		// Token: 0x0400150A RID: 5386
		IpTimeToLive,
		/// <summary>Set the interface for outgoing multicast packets.</summary>
		// Token: 0x0400150B RID: 5387
		MulticastInterface = 9,
		/// <summary>An IP multicast Time to Live.</summary>
		// Token: 0x0400150C RID: 5388
		MulticastTimeToLive,
		/// <summary>An IP multicast loopback.</summary>
		// Token: 0x0400150D RID: 5389
		MulticastLoopback,
		/// <summary>Add an IP group membership.</summary>
		// Token: 0x0400150E RID: 5390
		AddMembership,
		/// <summary>Drop an IP group membership.</summary>
		// Token: 0x0400150F RID: 5391
		DropMembership,
		/// <summary>Do not fragment IP datagrams.</summary>
		// Token: 0x04001510 RID: 5392
		DontFragment,
		/// <summary>Join a source group.</summary>
		// Token: 0x04001511 RID: 5393
		AddSourceMembership,
		/// <summary>Drop a source group.</summary>
		// Token: 0x04001512 RID: 5394
		DropSourceMembership,
		/// <summary>Block data from a source.</summary>
		// Token: 0x04001513 RID: 5395
		BlockSource,
		/// <summary>Unblock a previously blocked source.</summary>
		// Token: 0x04001514 RID: 5396
		UnblockSource,
		/// <summary>Return information about received packets.</summary>
		// Token: 0x04001515 RID: 5397
		PacketInformation,
		/// <summary>Specifies the maximum number of router hops for an Internet Protocol version 6 (IPv6) packet. This is similar to Time to Live (TTL) for Internet Protocol version 4.</summary>
		// Token: 0x04001516 RID: 5398
		HopLimit = 21,
		/// <summary>Enables restriction of a IPv6 socket to a specified scope, such as addresses with the same link local or site local prefix.This socket option enables applications to place access restrictions on IPv6 sockets. Such restrictions enable an application running on a private LAN to simply and robustly harden itself against external attacks. This socket option widens or narrows the scope of a listening socket, enabling unrestricted access from public and private users when appropriate, or restricting access only to the same site, as required. This socket option has defined protection levels specified in the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> enumeration.</summary>
		// Token: 0x04001517 RID: 5399
		IPProtectionLevel = 23,
		/// <summary>Indicates if a socket created for the AF_INET6 address family is restricted to IPv6 communications only. Sockets created for the AF_INET6 address family may be used for both IPv6 and IPv4 communications. Some applications may want to restrict their use of a socket created for the AF_INET6 address family to IPv6 communications only. When this value is non-zero (the default on Windows), a socket created for the AF_INET6 address family can be used to send and receive IPv6 packets only. When this value is zero, a socket created for the AF_INET6 address family can be used to send and receive packets to and from an IPv6 address or an IPv4 address. Note that the ability to interact with an IPv4 address requires the use of IPv4 mapped addresses. This socket option is supported on Windows Vista or later.</summary>
		// Token: 0x04001518 RID: 5400
		IPv6Only = 27,
		/// <summary>Disables the Nagle algorithm for send coalescing.</summary>
		// Token: 0x04001519 RID: 5401
		NoDelay = 1,
		/// <summary>Use urgent data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x0400151A RID: 5402
		BsdUrgent,
		/// <summary>Use expedited data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x0400151B RID: 5403
		Expedited = 2,
		/// <summary>Send UDP datagrams with checksum set to zero.</summary>
		// Token: 0x0400151C RID: 5404
		NoChecksum = 1,
		/// <summary>Set or get the UDP checksum coverage.</summary>
		// Token: 0x0400151D RID: 5405
		ChecksumCoverage = 20,
		/// <summary>Updates an accepted socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_ACCEPT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x0400151E RID: 5406
		UpdateAcceptContext = 28683,
		/// <summary>Updates a connected socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_CONNECT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x0400151F RID: 5407
		UpdateConnectContext = 28688
	}
}
