using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a non-fatal application error occurs.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BB RID: 187
	[Serializable]
	public class ApplicationException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class.</summary>
		// Token: 0x06000495 RID: 1173 RVA: 0x00018729 File Offset: 0x00016929
		public ApplicationException()
			: base("Error in the application.")
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error. </param>
		// Token: 0x06000496 RID: 1174 RVA: 0x00018741 File Offset: 0x00016941
		public ApplicationException(string message)
			: base(message)
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000497 RID: 1175 RVA: 0x00018755 File Offset: 0x00016955
		public ApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000498 RID: 1176 RVA: 0x0001876A File Offset: 0x0001696A
		protected ApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
