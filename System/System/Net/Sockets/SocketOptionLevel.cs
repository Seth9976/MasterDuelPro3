using System;

namespace System.Net.Sockets
{
	/// <summary>Defines socket option levels for the <see cref="M:System.Net.Sockets.Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName,System.Int32)" /> and <see cref="M:System.Net.Sockets.Socket.GetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName)" /> methods.</summary>
	// Token: 0x020004BB RID: 1211
	public enum SocketOptionLevel
	{
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply to all sockets.</summary>
		// Token: 0x040014EC RID: 5356
		Socket = 65535,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IP sockets.</summary>
		// Token: 0x040014ED RID: 5357
		IP = 0,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IPv6 sockets.</summary>
		// Token: 0x040014EE RID: 5358
		IPv6 = 41,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to TCP sockets.</summary>
		// Token: 0x040014EF RID: 5359
		Tcp = 6,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to UDP sockets.</summary>
		// Token: 0x040014F0 RID: 5360
		Udp = 17
	}
}
