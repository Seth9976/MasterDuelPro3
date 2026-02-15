using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Defines the base class for predefined exceptions in the <see cref="N:System" /> namespace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014C RID: 332
	[Serializable]
	public class SystemException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class.</summary>
		// Token: 0x06000B34 RID: 2868 RVA: 0x00032262 File Offset: 0x00030462
		public SystemException()
			: base("System error.")
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000B35 RID: 2869 RVA: 0x0003227A File Offset: 0x0003047A
		public SystemException(string message)
			: base(message)
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000B36 RID: 2870 RVA: 0x0003228E File Offset: 0x0003048E
		public SystemException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000B37 RID: 2871 RVA: 0x0001876A File Offset: 0x0001696A
		protected SystemException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
