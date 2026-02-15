using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a program contains invalid Microsoft intermediate language (MSIL) or metadata. Generally this indicates a bug in the compiler that generated the program.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000113 RID: 275
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with default properties.</summary>
		// Token: 0x06000910 RID: 2320 RVA: 0x00028014 File Offset: 0x00026214
		public InvalidProgramException()
			: base("Common Language Runtime detected an invalid program.")
		{
			base.HResult = -2146233030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000911 RID: 2321 RVA: 0x0002802C File Offset: 0x0002622C
		public InvalidProgramException(string message)
			: base(message)
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00018324 File Offset: 0x00016524
		internal InvalidProgramException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
