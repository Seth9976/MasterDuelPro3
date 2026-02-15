using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to divide an integral or decimal value by zero.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D8 RID: 216
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class.</summary>
		// Token: 0x0600074B RID: 1867 RVA: 0x0001E222 File Offset: 0x0001C422
		public DivideByZeroException()
			: base("Attempted to divide by zero.")
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x0600074C RID: 1868 RVA: 0x0001E23A File Offset: 0x0001C43A
		public DivideByZeroException(string message)
			: base(message)
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600074D RID: 1869 RVA: 0x0001E24E File Offset: 0x0001C44E
		public DivideByZeroException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600074E RID: 1870 RVA: 0x0001E263 File Offset: 0x0001C463
		protected DivideByZeroException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
