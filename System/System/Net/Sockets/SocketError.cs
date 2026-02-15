using System;

namespace System.Net.Sockets
{
	/// <summary>Defines error codes for the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x020004B9 RID: 1209
	public enum SocketError
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> operation succeeded.</summary>
		// Token: 0x040014B1 RID: 5297
		Success,
		/// <summary>An unspecified <see cref="T:System.Net.Sockets.Socket" /> error has occurred.</summary>
		// Token: 0x040014B2 RID: 5298
		SocketError = -1,
		/// <summary>A blocking <see cref="T:System.Net.Sockets.Socket" /> call was canceled.</summary>
		// Token: 0x040014B3 RID: 5299
		Interrupted = 10004,
		/// <summary>An attempt was made to access a <see cref="T:System.Net.Sockets.Socket" /> in a way that is forbidden by its access permissions.</summary>
		// Token: 0x040014B4 RID: 5300
		AccessDenied = 10013,
		/// <summary>An invalid pointer address was detected by the underlying socket provider.</summary>
		// Token: 0x040014B5 RID: 5301
		Fault,
		/// <summary>An invalid argument was supplied to a <see cref="T:System.Net.Sockets.Socket" /> member.</summary>
		// Token: 0x040014B6 RID: 5302
		InvalidArgument = 10022,
		/// <summary>There are too many open sockets in the underlying socket provider.</summary>
		// Token: 0x040014B7 RID: 5303
		TooManyOpenSockets = 10024,
		/// <summary>An operation on a nonblocking socket cannot be completed immediately.</summary>
		// Token: 0x040014B8 RID: 5304
		WouldBlock = 10035,
		/// <summary>A blocking operation is in progress.</summary>
		// Token: 0x040014B9 RID: 5305
		InProgress,
		/// <summary>The nonblocking <see cref="T:System.Net.Sockets.Socket" /> already has an operation in progress.</summary>
		// Token: 0x040014BA RID: 5306
		AlreadyInProgress,
		/// <summary>A <see cref="T:System.Net.Sockets.Socket" /> operation was attempted on a non-socket.</summary>
		// Token: 0x040014BB RID: 5307
		NotSocket,
		/// <summary>A required address was omitted from an operation on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x040014BC RID: 5308
		DestinationAddressRequired,
		/// <summary>The datagram is too long.</summary>
		// Token: 0x040014BD RID: 5309
		MessageSize,
		/// <summary>The protocol type is incorrect for this <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x040014BE RID: 5310
		ProtocolType,
		/// <summary>An unknown, invalid, or unsupported option or level was used with a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x040014BF RID: 5311
		ProtocolOption,
		/// <summary>The protocol is not implemented or has not been configured.</summary>
		// Token: 0x040014C0 RID: 5312
		ProtocolNotSupported,
		/// <summary>The support for the specified socket type does not exist in this address family.</summary>
		// Token: 0x040014C1 RID: 5313
		SocketNotSupported,
		/// <summary>The address family is not supported by the protocol family.</summary>
		// Token: 0x040014C2 RID: 5314
		OperationNotSupported,
		/// <summary>The protocol family is not implemented or has not been configured.</summary>
		// Token: 0x040014C3 RID: 5315
		ProtocolFamilyNotSupported,
		/// <summary>The address family specified is not supported. This error is returned if the IPv6 address family was specified and the IPv6 stack is not installed on the local machine. This error is returned if the IPv4 address family was specified and the IPv4 stack is not installed on the local machine.</summary>
		// Token: 0x040014C4 RID: 5316
		AddressFamilyNotSupported,
		/// <summary>Only one use of an address is normally permitted.</summary>
		// Token: 0x040014C5 RID: 5317
		AddressAlreadyInUse,
		/// <summary>The selected IP address is not valid in this context.</summary>
		// Token: 0x040014C6 RID: 5318
		AddressNotAvailable,
		/// <summary>The network is not available.</summary>
		// Token: 0x040014C7 RID: 5319
		NetworkDown,
		/// <summary>No route to the remote host exists.</summary>
		// Token: 0x040014C8 RID: 5320
		NetworkUnreachable,
		/// <summary>The application tried to set <see cref="F:System.Net.Sockets.SocketOptionName.KeepAlive" /> on a connection that has already timed out.</summary>
		// Token: 0x040014C9 RID: 5321
		NetworkReset,
		/// <summary>The connection was aborted by the .NET Framework or the underlying socket provider.</summary>
		// Token: 0x040014CA RID: 5322
		ConnectionAborted,
		/// <summary>The connection was reset by the remote peer.</summary>
		// Token: 0x040014CB RID: 5323
		ConnectionReset,
		/// <summary>No free buffer space is available for a <see cref="T:System.Net.Sockets.Socket" /> operation.</summary>
		// Token: 0x040014CC RID: 5324
		NoBufferSpaceAvailable,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is already connected.</summary>
		// Token: 0x040014CD RID: 5325
		IsConnected,
		/// <summary>The application tried to send or receive data, and the <see cref="T:System.Net.Sockets.Socket" /> is not connected.</summary>
		// Token: 0x040014CE RID: 5326
		NotConnected,
		/// <summary>A request to send or receive data was disallowed because the <see cref="T:System.Net.Sockets.Socket" /> has already been closed.</summary>
		// Token: 0x040014CF RID: 5327
		Shutdown,
		/// <summary>The connection attempt timed out, or the connected host has failed to respond.</summary>
		// Token: 0x040014D0 RID: 5328
		TimedOut = 10060,
		/// <summary>The remote host is actively refusing a connection.</summary>
		// Token: 0x040014D1 RID: 5329
		ConnectionRefused,
		/// <summary>The operation failed because the remote host is down.</summary>
		// Token: 0x040014D2 RID: 5330
		HostDown = 10064,
		/// <summary>There is no network route to the specified host.</summary>
		// Token: 0x040014D3 RID: 5331
		HostUnreachable,
		/// <summary>Too many processes are using the underlying socket provider.</summary>
		// Token: 0x040014D4 RID: 5332
		ProcessLimit = 10067,
		/// <summary>The network subsystem is unavailable.</summary>
		// Token: 0x040014D5 RID: 5333
		SystemNotReady = 10091,
		/// <summary>The version of the underlying socket provider is out of range.</summary>
		// Token: 0x040014D6 RID: 5334
		VersionNotSupported,
		/// <summary>The underlying socket provider has not been initialized.</summary>
		// Token: 0x040014D7 RID: 5335
		NotInitialized,
		/// <summary>A graceful shutdown is in progress.</summary>
		// Token: 0x040014D8 RID: 5336
		Disconnecting = 10101,
		/// <summary>The specified class was not found.</summary>
		// Token: 0x040014D9 RID: 5337
		TypeNotFound = 10109,
		/// <summary>No such host is known. The name is not an official host name or alias.</summary>
		// Token: 0x040014DA RID: 5338
		HostNotFound = 11001,
		/// <summary>The name of the host could not be resolved. Try again later.</summary>
		// Token: 0x040014DB RID: 5339
		TryAgain,
		/// <summary>The error is unrecoverable or the requested database cannot be located.</summary>
		// Token: 0x040014DC RID: 5340
		NoRecovery,
		/// <summary>The requested name or IP address was not found on the name server.</summary>
		// Token: 0x040014DD RID: 5341
		NoData,
		/// <summary>The application has initiated an overlapped operation that cannot be completed immediately.</summary>
		// Token: 0x040014DE RID: 5342
		IOPending = 997,
		/// <summary>The overlapped operation was aborted due to the closure of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x040014DF RID: 5343
		OperationAborted = 995
	}
}
