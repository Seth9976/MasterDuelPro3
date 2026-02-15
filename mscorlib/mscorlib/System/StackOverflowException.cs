using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the execution stack overflows because it contains too many nested method calls. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000144 RID: 324
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "The requested operation caused a stack overflow." This message takes into account the current system culture.</summary>
		// Token: 0x06000B07 RID: 2823 RVA: 0x00031D37 File Offset: 0x0002FF37
		public StackOverflowException()
			: base("Operation caused a stack overflow.")
		{
			base.HResult = -2147023895;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of message is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x06000B08 RID: 2824 RVA: 0x00031D4F File Offset: 0x0002FF4F
		public StackOverflowException(string message)
			: base(message)
		{
			base.HResult = -2147023895;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000B09 RID: 2825 RVA: 0x00031D63 File Offset: 0x0002FF63
		public StackOverflowException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147023895;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00018324 File Offset: 0x00016524
		internal StackOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
