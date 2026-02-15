using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method call is invalid for the object's current state.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class.</summary>
		// Token: 0x0600090C RID: 2316 RVA: 0x00027FD3 File Offset: 0x000261D3
		public InvalidOperationException()
			: base("Operation is not valid due to the current state of the object.")
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600090D RID: 2317 RVA: 0x00027FEB File Offset: 0x000261EB
		public InvalidOperationException(string message)
			: base(message)
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600090E RID: 2318 RVA: 0x00027FFF File Offset: 0x000261FF
		public InvalidOperationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600090F RID: 2319 RVA: 0x00018324 File Offset: 0x00016524
		protected InvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
