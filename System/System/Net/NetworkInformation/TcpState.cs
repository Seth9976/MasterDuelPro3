using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the states of a Transmission Control Protocol (TCP) connection.</summary>
	// Token: 0x0200045E RID: 1118
	public enum TcpState
	{
		/// <summary>The TCP connection state is unknown.</summary>
		// Token: 0x040012D6 RID: 4822
		Unknown,
		/// <summary>The TCP connection is closed.</summary>
		// Token: 0x040012D7 RID: 4823
		Closed,
		/// <summary>The local endpoint of the TCP connection is listening for a connection request from any remote endpoint.</summary>
		// Token: 0x040012D8 RID: 4824
		Listen,
		/// <summary>The local endpoint of the TCP connection has sent the remote endpoint a segment header with the synchronize (SYN) control bit set and is waiting for a matching connection request.</summary>
		// Token: 0x040012D9 RID: 4825
		SynSent,
		/// <summary>The local endpoint of the TCP connection has sent and received a connection request and is waiting for an acknowledgment.</summary>
		// Token: 0x040012DA RID: 4826
		SynReceived,
		/// <summary>The TCP handshake is complete. The connection has been established and data can be sent.</summary>
		// Token: 0x040012DB RID: 4827
		Established,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint or for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040012DC RID: 4828
		FinWait1,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint.</summary>
		// Token: 0x040012DD RID: 4829
		FinWait2,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the local user.</summary>
		// Token: 0x040012DE RID: 4830
		CloseWait,
		/// <summary>The local endpoint of the TCP connection is waiting for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040012DF RID: 4831
		Closing,
		/// <summary>The local endpoint of the TCP connection is waiting for the final acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040012E0 RID: 4832
		LastAck,
		/// <summary>The local endpoint of the TCP connection is waiting for enough time to pass to ensure that the remote endpoint received the acknowledgement of its connection termination request.</summary>
		// Token: 0x040012E1 RID: 4833
		TimeWait,
		/// <summary>The transmission control buffer (TCB) for the TCP connection is being deleted.</summary>
		// Token: 0x040012E2 RID: 4834
		DeleteTcb
	}
}
