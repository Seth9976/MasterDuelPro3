using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the format of an argument does not meet the parameter specifications of the invoked method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E3 RID: 227
	[Serializable]
	public class FormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class.</summary>
		// Token: 0x0600078D RID: 1933 RVA: 0x0001E784 File Offset: 0x0001C984
		public FormatException()
			: base("One of the identified items was in an invalid format.")
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600078E RID: 1934 RVA: 0x0001E79C File Offset: 0x0001C99C
		public FormatException(string message)
			: base(message)
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600078F RID: 1935 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		public FormatException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000790 RID: 1936 RVA: 0x00018324 File Offset: 0x00016524
		protected FormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
