using System;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs while accessing the network through a pluggable protocol.</summary>
	// Token: 0x020003BA RID: 954
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class.</summary>
		// Token: 0x060017B4 RID: 6068 RVA: 0x00064F45 File Offset: 0x00063145
		public WebException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message.</summary>
		/// <param name="message">The text of the error message. </param>
		// Token: 0x060017B5 RID: 6069 RVA: 0x00064F55 File Offset: 0x00063155
		public WebException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and nested exception.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		// Token: 0x060017B6 RID: 6070 RVA: 0x00064F5F File Offset: 0x0006315F
		public WebException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and status.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		// Token: 0x060017B7 RID: 6071 RVA: 0x00064F71 File Offset: 0x00063171
		public WebException(string message, WebExceptionStatus status)
			: this(message, null, status, null)
		{
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00064F7D File Offset: 0x0006317D
		internal WebException(string message, WebExceptionStatus status, WebExceptionInternalStatus internalStatus, Exception innerException)
			: this(message, innerException, status, null, internalStatus)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message, nested exception, status, and response.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> instance that contains the response from the remote host. </param>
		// Token: 0x060017B9 RID: 6073 RVA: 0x00064F8B File Offset: 0x0006318B
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response)
			: this(message, null, innerException, status, response)
		{
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00064F9C File Offset: 0x0006319C
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00064FE8 File Offset: 0x000631E8
		internal WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: this(message, null, innerException, status, response, internalStatus)
		{
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00064FF8 File Offset: 0x000631F8
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
			this.m_InternalStatus = internalStatus;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebException" />. </param>
		// Token: 0x060017BD RID: 6077 RVA: 0x0006504C File Offset: 0x0006324C
		protected WebException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebException" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x060017BE RID: 6078 RVA: 0x0006505E File Offset: 0x0006325E
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060017BF RID: 6079 RVA: 0x0001A2DA File Offset: 0x000184DA
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.WebExceptionStatus" /> values.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x00065068 File Offset: 0x00063268
		public WebExceptionStatus Status
		{
			get
			{
				return this.m_Status;
			}
		}

		/// <summary>Gets the response that the remote host returned.</summary>
		/// <returns>If a response is available from the Internet resource, a <see cref="T:System.Net.WebResponse" /> instance that contains the error response from an Internet resource; otherwise, null.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x00065070 File Offset: 0x00063270
		public WebResponse Response
		{
			get
			{
				return this.m_Response;
			}
		}

		// Token: 0x04000EE0 RID: 3808
		private WebExceptionStatus m_Status = WebExceptionStatus.UnknownError;

		// Token: 0x04000EE1 RID: 3809
		private WebResponse m_Response;

		// Token: 0x04000EE2 RID: 3810
		[NonSerialized]
		private WebExceptionInternalStatus m_InternalStatus;
	}
}
