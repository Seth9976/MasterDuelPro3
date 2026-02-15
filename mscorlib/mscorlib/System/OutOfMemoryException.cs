using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is not enough memory to continue the execution of a program.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000180 RID: 384
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class.</summary>
		// Token: 0x06000DBC RID: 3516 RVA: 0x0003A2F4 File Offset: 0x000384F4
		public OutOfMemoryException()
			: base("Insufficient memory to continue the execution of the program.")
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000DBD RID: 3517 RVA: 0x0003A30C File Offset: 0x0003850C
		public OutOfMemoryException(string message)
			: base(message)
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000DBE RID: 3518 RVA: 0x0003A320 File Offset: 0x00038520
		public OutOfMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000DBF RID: 3519 RVA: 0x00018324 File Offset: 0x00016524
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
