using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies socket send and receive behaviors.</summary>
	// Token: 0x020004BA RID: 1210
	[Flags]
	public enum SocketFlags
	{
		/// <summary>Use no flags for this call.</summary>
		// Token: 0x040014E1 RID: 5345
		None = 0,
		/// <summary>Process out-of-band data.</summary>
		// Token: 0x040014E2 RID: 5346
		OutOfBand = 1,
		/// <summary>Peek at the incoming message.</summary>
		// Token: 0x040014E3 RID: 5347
		Peek = 2,
		/// <summary>Send without using routing tables.</summary>
		// Token: 0x040014E4 RID: 5348
		DontRoute = 4,
		/// <summary>Provides a standard value for the number of WSABUF structures that are used to send and receive data. This value is not used or supported on .NET Framework 4.5.</summary>
		// Token: 0x040014E5 RID: 5349
		MaxIOVectorLength = 16,
		/// <summary>The message was too large to fit into the specified buffer and was truncated.</summary>
		// Token: 0x040014E6 RID: 5350
		Truncated = 256,
		/// <summary>Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.</summary>
		// Token: 0x040014E7 RID: 5351
		ControlDataTruncated = 512,
		/// <summary>Indicates a broadcast packet.</summary>
		// Token: 0x040014E8 RID: 5352
		Broadcast = 1024,
		/// <summary>Indicates a multicast packet.</summary>
		// Token: 0x040014E9 RID: 5353
		Multicast = 2048,
		/// <summary>Partial send or receive for message.</summary>
		// Token: 0x040014EA RID: 5354
		Partial = 32768
	}
}
