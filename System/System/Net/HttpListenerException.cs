using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs processing an HTTP request.</summary>
	// Token: 0x020003A6 RID: 934
	[Serializable]
	public class HttpListenerException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class. </summary>
		// Token: 0x06001763 RID: 5987 RVA: 0x0004C074 File Offset: 0x0004A274
		public HttpListenerException()
			: base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code and message.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06001764 RID: 5988 RVA: 0x00063F10 File Offset: 0x00062110
		public HttpListenerException(int errorCode, string message)
			: base(errorCode, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to deserialize the new <see cref="T:System.Net.HttpListenerException" /> object. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object. </param>
		// Token: 0x06001765 RID: 5989 RVA: 0x00063F1A File Offset: 0x0006211A
		protected HttpListenerException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
