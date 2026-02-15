using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a check for sufficient available memory fails. This class cannot be inherited.</summary>
	// Token: 0x0200017D RID: 381
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000DAF RID: 3503 RVA: 0x0003A0F7 File Offset: 0x000382F7
		public InsufficientMemoryException()
			: base("Insufficient memory to continue the execution of the program.")
		{
			base.HResult = -2146233027;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003A10F File Offset: 0x0003830F
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
