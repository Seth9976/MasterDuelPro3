using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown for errors in an arithmetic, casting, or conversion operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BF RID: 191
	[Serializable]
	public class ArithmeticException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class.</summary>
		// Token: 0x060004AC RID: 1196 RVA: 0x000189AD File Offset: 0x00016BAD
		public ArithmeticException()
			: base("Overflow or underflow in the arithmetic operation.")
		{
			base.HResult = -2147024362;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x060004AD RID: 1197 RVA: 0x000189C5 File Offset: 0x00016BC5
		public ArithmeticException(string message)
			: base(message)
		{
			base.HResult = -2147024362;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060004AE RID: 1198 RVA: 0x000189D9 File Offset: 0x00016BD9
		public ArithmeticException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024362;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060004AF RID: 1199 RVA: 0x00018324 File Offset: 0x00016524
		protected ArithmeticException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
