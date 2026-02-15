using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>The exception that is thrown when a socket error occurs.</summary>
	// Token: 0x020004B0 RID: 1200
	[Serializable]
	public class SocketException : Win32Exception
	{
		// Token: 0x06001DCC RID: 7628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WSAGetLastError_icall();

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the last operating system error code.</summary>
		// Token: 0x06001DCD RID: 7629 RVA: 0x00081C05 File Offset: 0x0007FE05
		public SocketException()
			: base(SocketException.WSAGetLastError_icall())
		{
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00063F10 File Offset: 0x00062110
		internal SocketException(int error, string message)
			: base(error, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the specified error code.</summary>
		/// <param name="errorCode">The error code that indicates the error that occurred. </param>
		// Token: 0x06001DCF RID: 7631 RVA: 0x0007BEA9 File Offset: 0x0007A0A9
		public SocketException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0007BEA9 File Offset: 0x0007A0A9
		internal SocketException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		// Token: 0x06001DD1 RID: 7633 RVA: 0x00063F1A File Offset: 0x0006211A
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the error message that is associated with this exception.</summary>
		/// <returns>A string that contains the error message. </returns>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x00081C12 File Offset: 0x0007FE12
		public override string Message
		{
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x00081C3E File Offset: 0x0007FE3E
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x0400143C RID: 5180
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
