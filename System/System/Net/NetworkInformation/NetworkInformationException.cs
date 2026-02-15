using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when an error occurs while retrieving network information.</summary>
	// Token: 0x0200045A RID: 1114
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class.</summary>
		// Token: 0x06001C39 RID: 7225 RVA: 0x0004C074 File Offset: 0x0004A274
		public NetworkInformationException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with the specified error code.</summary>
		/// <param name="errorCode">A Win32 error code. </param>
		// Token: 0x06001C3A RID: 7226 RVA: 0x0007BEA9 File Offset: 0x0007A0A9
		public NetworkInformationException(int errorCode)
			: base(errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">A SerializationInfo object that contains the serialized exception data. </param>
		/// <param name="streamingContext">A StreamingContext that contains contextual information about the serialized exception. </param>
		// Token: 0x06001C3B RID: 7227 RVA: 0x00063F1A File Offset: 0x0006211A
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
