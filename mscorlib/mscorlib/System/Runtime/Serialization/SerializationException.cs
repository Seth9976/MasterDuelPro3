using System;

namespace System.Runtime.Serialization
{
	/// <summary>The exception thrown when an error occurs during serialization or deserialization.</summary>
	// Token: 0x020004A8 RID: 1192
	[Serializable]
	public class SerializationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with default properties.</summary>
		// Token: 0x0600262D RID: 9773 RVA: 0x0009A71F File Offset: 0x0009891F
		public SerializationException()
			: base(SerializationException.s_nullMessage)
		{
			base.HResult = -2146233076;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified message.</summary>
		/// <param name="message">Indicates the reason why the exception occurred. </param>
		// Token: 0x0600262E RID: 9774 RVA: 0x0009A737 File Offset: 0x00098937
		public SerializationException(string message)
			: base(message)
		{
			base.HResult = -2146233076;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600262F RID: 9775 RVA: 0x0009A74B File Offset: 0x0009894B
		public SerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233076;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class from serialized data.</summary>
		/// <param name="info">The serialization information object holding the serialized object data in the name-value form. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		// Token: 0x06002630 RID: 9776 RVA: 0x00018324 File Offset: 0x00016524
		protected SerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04001243 RID: 4675
		private static string s_nullMessage = "Serialization error.";
	}
}
